import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchGetAllCarAsyncAction, fetchGetAllRentingAsyncAction } from '../redux/Thunk';
import { selectAvailableCars } from '../redux/getAllCarSlice';

const RentCar = () => {
  const dispatch = useDispatch();
  const cars = useSelector(state => state.car.cars);
  const rentals = useSelector(state => state.car.rentals);
  const loading = useSelector(state => state.car.loading);
  const error = useSelector(state => state.car.error);

  const [selectedSeats, setSelectedSeats] = useState(null);
  const [selectedCity, setSelectedCity] = useState(null);
  const [selectedStreet, setSelectedStreet] = useState(null);
  const [startTime, setStartTime] = useState(null);
  const [endTime, setEndTime] = useState(null);
  const [showSeatsModal, setShowSeatsModal] = useState(false);
  const [showLocationModal, setShowLocationModal] = useState(false);
  const [showTimeModal, setShowTimeModal] = useState(false);
  const [showAvailableOnly, setShowAvailableOnly] = useState(false);
  const [filteredCars, setFilteredCars] = useState([]);

  useEffect(() => {
    dispatch(fetchGetAllCarAsyncAction());
    dispatch(fetchGetAllRentingAsyncAction());
  }, [dispatch]);

  const handleFilter = () => {
    const payload = {
      startTime: startTime || undefined,
      endTime: endTime || undefined,
      selectedSeats: selectedSeats || undefined,
      selectedCity: selectedCity || undefined,
      selectedStreet: selectedStreet || undefined,
    };

    setFilteredCars( dispatch(selectAvailableCars(payload)));
  };

  useEffect(() => {
    handleFilter();
    const cities = [...new Set(filteredCars.map(car => car.city))];
    const streets = [...new Set(filteredCars.filter(car => car.city === selectedCity).map(car => car.street))];

    if (loading) return;
    if (error) return;

  }, [dispatch, cars, selectedCity, selectedStreet, startTime, endTime, selectedSeats]);

  return (
    <div>
      <h1>Available Cars</h1>
      <button onClick={() => setShowSeatsModal(true)}>Select Seats</button>
      <button onClick={() => setShowLocationModal(true)}>Select Location</button>
      <button onClick={() => setShowTimeModal(true)}>Select Time</button>
      <label>
        <input
          type="checkbox"
          checked={showAvailableOnly}
          onChange={() => setShowAvailableOnly(!showAvailableOnly)}
        />
        Show Available Cars Only
      </label>

      {showSeatsModal && (
        <div className="modal">
          <button onClick={() => { setSelectedSeats(5); setShowSeatsModal(false); }}>5 Seats</button>
          <button onClick={() => { setSelectedSeats(7); setShowSeatsModal(false); }}>7 Seats</button>
          <button onClick={() => { setSelectedSeats(9); setShowSeatsModal(false); }}>9 Seats</button>
          <button onClick={() => setShowSeatsModal(false)}>Close</button>
        </div>
      )}

      {showLocationModal && (
        <div className="modal">
          <h2>Select City and Street</h2>
          <select onChange={(e) => setSelectedCity(e.target.value)}>
            <option value="">Select City</option>
            {cities.map(city => <option key={city} value={city}>{city}</option>)}
          </select>
          {selectedCity && (
            <select onChange={(e) => setSelectedStreet(e.target.value)}>
              <option value="">Select Street</option>
              {streets.map(street => <option key={street} value={street}>{street}</option>)}
            </select>
          )}
          <button onClick={() => setShowLocationModal(false)}>Close</button>
        </div>
      )}

      {showTimeModal && (
        <div className="modal">
          <h2>Select Time</h2>
          <input type="datetime-local" onChange={(e) => setStartTime(e.target.value)} />
          <input type="datetime-local" onChange={(e) => setEndTime(e.target.value)} />
          <button onClick={() => setShowTimeModal(false)}>Close</button>
        </div>
      )}

      <ul>
        {filteredCars.length > 0 ? (
          filteredCars.map(car => (
            <li key={car.id}>
              {car.name} - {car.seats} seats - {car.city}
              <button onClick={() => {/* כאן תזמן את קומפוננטת ההשכרה */}}>
                Rent
              </button>
            </li>
          ))
        ) : (
          <li>אין רכבים זמינים</li>
        )}
      </ul>
    </div>
  );
};

export default RentCar;
