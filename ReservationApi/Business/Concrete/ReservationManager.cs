using ReservationApi.Business.Abstract;
using ReservationApi.Data;
using ReservationApi.Models;

namespace ReservationApi.Business.Concrete
{
    public class ReservationManager : IReservationService
    {
        private IAppRepository _appRepository;

        public ReservationManager(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public void Add(Reservation reservation)
        {
            var train = _appRepository.GetTrainById(reservation.TrainId);
            var waggon = calculateNumberofMaxEmptySeat(train);

            _appRepository.Add(reservation);
            _appRepository.SaveAll();

            ReservationResponse reservationResponse = new ReservationResponse
            {
                IsReservable = false,
                ReservationId = reservation.Id,
            };

            _appRepository.Add(reservationResponse);
            _appRepository.SaveAll();

            if (calculateSeventyPercent(waggon, reservation.NumberofPassenger))
            {
                reservationResponse.IsReservable = true;

                PlacementDetail placementDetail = new PlacementDetail
                {
                    ReservationResponseId = reservationResponse.Id,
                    NumberofPeople = reservation.NumberofPassenger,
                    WaggonId = waggon.Id,
                };
                _appRepository.Add(placementDetail);
                waggon.NumberofReservedSeats += reservation.NumberofPassenger;
                _appRepository.Update(waggon);

            }
            else
            {
                if (reservation.IsAllowDifferentWaggons)
                    reserveDifferentWaggons(reservation, reservationResponse, train);
            }
            _appRepository.GetPlacementDetails(reservationResponse.Id).ForEach(placementDetail => _appRepository.Update(placementDetail));
            _appRepository.SaveAll();
        }



        public Waggon calculateNumberofMaxEmptySeat(Train train)
        {
            Waggon haveMaxEmptySeat = train.Waggons[0];
            foreach (var waggon in train.Waggons)
            {
                if ((haveMaxEmptySeat.Capacity - haveMaxEmptySeat.NumberofReservedSeats) < (waggon.Capacity - waggon.NumberofReservedSeats))
                {
                    haveMaxEmptySeat = waggon;
                }
            }
            return haveMaxEmptySeat;
        }

        /// <summary>
        ///  This method compares the number of reserved seats and the number of passengers 
        ///  in the reservation request with the wagon capacity and returns a true false value.
        /// </summary>
        public bool calculateSeventyPercent(Waggon waggon, int numberofPassanger)
        {
            if ((double)(waggon.NumberofReservedSeats + numberofPassanger) / waggon.Capacity < 0.7)
                return true;
            if(numberofPassanger==0)
                return true;
            return false;
        }

        public void reserveDifferentWaggons(Reservation reservation, ReservationResponse reservationResponse, Train train)
        {
            int passangerCount;
            var remainder = reservation.NumberofPassenger;
            int iteration = 0;
            foreach (var waggon1 in train.Waggons)
            {
                iteration++;
                passangerCount = remainder;
                while (!calculateSeventyPercent(waggon1, passangerCount))
                {
                    passangerCount--;
                }
                if (passangerCount > 0 && passangerCount <= remainder)
                {
                    PlacementDetail placementDetail = new PlacementDetail
                    {
                        NumberofPeople = passangerCount,
                        WaggonId = waggon1.Id,
                        ReservationResponseId = reservationResponse.Id,
                    };
                    _appRepository.Add(placementDetail);
                    _appRepository.SaveAll();
                    remainder -= passangerCount;
                }
                if (remainder == 0)
                {
                    reservationResponse.IsReservable = true;
                    foreach (var placementDetail in _appRepository.GetPlacementDetails(reservationResponse.Id))
                    {
                        var waggon2 = _appRepository.GetWaggonById(placementDetail.WaggonId);
                        waggon2.NumberofReservedSeats += placementDetail.NumberofPeople;
                        _appRepository.Update(waggon2);
                    }
                    break;
                }
                else if (remainder > 0 && iteration == train.Waggons.Count)
                {
                    foreach (var placementDetail in _appRepository.GetPlacementDetails(reservationResponse.Id))
                    {
                        _appRepository.Delete(placementDetail);
                        _appRepository.SaveAll();
                    }
                }
            }
        }

    }
}
