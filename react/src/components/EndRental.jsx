import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { fetcheEndReantlasyncAction } from "../redux/Thunk";
import { useNavigate, useLocation } from 'react-router-dom';
import "./AppTheme.css";
const EndRental = () => {
    const location = useLocation();
    const { rental} = location.state
    const dispatch = useDispatch();
    const navigate = useNavigate();


    const handleCancel = () => {
        navigate(-1); // חוזר למסך הקודם
    };

    const handleConfirm = async () => {
        const resultAction =  dispatch(fetcheEndReantlasyncAction({customerId:rental.idCustomer, rentalId:rental.id}));
        if(fetcheEndReantlasyncAction.fulfilled ()){
            console.log('Rental ended successfully:', resultAction.payload);
           navigate('/SuccessedEnd')
        } else {
            console.error('Failed to end rental:', resultAction.payload);
            <p>{resultAction.payload}</p>

        }
        }


    return (
        <div>
            <h2>האם אתה בטוח שאתה רוצה לסיים את ההשכרת רכב  {rental.idCar}?</h2>
            <button onClick={handleCancel}>ביטול</button>
            <button onClick={handleConfirm}>אישור</button>
        </div>
    );
};

export default EndRental
