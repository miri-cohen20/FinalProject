import { useDispatch, useSelector } from "react-redux";
import { fetchActivityRentals, fetchActivityAndFutureRentals, fetchGetAllCarAsyncAction } from "../redux/Thunk";
import React, { useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import { logout } from "../redux/sighSlice";

const ActivityRental = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const activityRentals = useSelector(state => state.activity.activityRentals);
    const activityAndFutueRentals = useSelector(state => state.activity.activityAndFutueRentals);
    const allCar = useSelector(state => state.car.cars);
    const idCustomer = useSelector(state => state.sigh.user.id);
    const loading = useSelector(state => state.activity.loading);
   
    useEffect(() => {
        const fetchData = async () => {
            dispatch(fetchGetAllCarAsyncAction());
            dispatch(fetchActivityRentals(idCustomer));
            dispatch(fetchActivityAndFutureRentals(idCustomer));
        };
        fetchData();
    }, [dispatch, idCustomer]);

    const handleRentalClick = (rental) => {
        const seatsValue = seats(rental.idCar);
        console.log(seatsValue);
        const streetValue = street(rental.idCar);
        console.log(streetValue);
        const cityValue = city(rental.idCar);
        console.log(cityValue);
        const state = { seats: seatsValue, street: streetValue, city: cityValue, rental }; 
        if (activityRentals.some(r => r.id === rental.id)) {
            navigate("/ActivityNow", { state });
        } else {
            navigate("/ActivityFuture", { state });
        }
    };

    const seats = (idCar) => {
        const car = allCar.find(c => c.id === idCar);
        return car ? car.seats : 0; // החזרת 0 אם הרכב לא נמצא
    };

    const city = (idCar) => {
        const car = allCar.find(c => c.id === idCar);
        return car ? car.city : "Unknown"; // החזרת "Unknown" אם הרכב לא נמצא
    };
    const street = (idCar) => {
        const car = allCar.find(c => c.id === idCar);
        return car ? car.street : "Unknown"; // החזרת "Unknown" אם הרכב לא נמצא
    };





    if (loading) {
        return <div>Loading...</div>; // הצגת הודעת טעינה
    }

    return (
        <>
            <h2>Activity Rentals</h2>
            <ul>
                {activityAndFutueRentals.map(rental => (
                    <li key={rental.id} onClick={() => handleRentalClick(rental)}>
                        car ID: {rental.idCar}, seats: {seats(rental.idCar)}, city: {city(rental.idCar)}
                    </li>
                ))}
            </ul>
        </>
    );
};

export default ActivityRental;
