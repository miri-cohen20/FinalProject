import { useDispatch, useSelector } from "react-redux";
import { fetchActivityRentals, fetchActivityAndFutureRentals, fetchGetAllCarAsyncAction } from "../redux/Thunk";
import React, { useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import { logout } from "../redux/sighSlice";
import "./AppTheme.css";

const ActivityRental = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const activityRentals = useSelector(state => state.activity.activityRentals);
    const activityAndFutueRentals = useSelector(state => state.activity.activityAndFutueRentals);
    const allCar = useSelector(state => state.car.cars);
    const idCustomer = useSelector(state => state.sigh.user.id);
    const loading = useSelector(state => state.activity.loading);

    useEffect(() => {
        dispatch(fetchGetAllCarAsyncAction());
        dispatch(fetchActivityRentals(idCustomer));
        dispatch(fetchActivityAndFutureRentals(idCustomer));
    }, [dispatch, idCustomer]);

    const handleRentalClick = (rental) => {
        const seatsValue = seats(rental.idCar);
        const streetValue = street(rental.idCar);
        const cityValue = city(rental.idCar);
        const state = { seats: seatsValue, street: streetValue, city: cityValue, rental };
        if (activityRentals.some(r => r.id === rental.id)) {
            navigate("/ActivityNow", { state });
        } else {
            navigate("/ActivityFuture", { state });
        }
    };

    const seats = (idCar) => {
        const car = allCar.find(c => c.id === idCar);
        return car ? car.seats : 0;
    };

    const city = (idCar) => {
        const car = allCar.find(c => c.id === idCar);
        return car ? car.city : "לא ידוע";
    };

    const street = (idCar) => {
        const car = allCar.find(c => c.id === idCar);
        return car ? car.street : "לא ידוע";
    };

    if (loading) {
        return (
            <div className="activity-loading-container">
                <div className="activity-loader"></div>
                <div className="activity-loading-text">טוען נתונים...</div>
            </div>
        );
    }

    return (
        <div className="activity-container">
            <h2 className="activity-title">השכרות פעילות ועתידיות</h2>
            <div className="activity-list">
                {activityAndFutueRentals.map(rental => (
                    <div key={rental.id} className="activity-card">
                        <div className="activity-icon">🚗</div>
                        <div className="activity-info">
                            <div><b>מספר רכב:</b> <span className="activity-value">{rental.idCar}</span></div>
                            <div><b>מושבים:</b> <span className="activity-value">{seats(rental.idCar)}</span></div>
                            <div><b>עיר:</b> <span className="activity-value">{city(rental.idCar)}</span></div>
                            <div><b>רחוב:</b> <span className="activity-value">{street(rental.idCar)}</span></div>
                        </div>
                        <button
                            className="activity-button"
                            onClick={() => handleRentalClick(rental)}
                        >
                            צפייה בפרטי השכרה
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ActivityRental;