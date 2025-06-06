import { useLocation } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

import "./AppTheme.css";
const ActivityFuture = () => {
    const navigate = useNavigate()
    const location = useLocation();
    const { rental, seats, street,city } = location.state || {}; // קבלת פרטי ההשכרה
  

    if (!rental) {
        return <div>No rental details available.</div>; // טיפול במקרה שאין פרטים
    }
    const handleExtendRental = () => {
        navigate("/ExtendRental", { state: { rental } }); // ניווט לדף סיום השכרה עם פרטי השכרה
    };
    return (
        <div>
            <h2>Rental Details</h2>
            <p>Car ID: {rental.idCar}</p>
            <p>Rental ID: {rental.id}</p>            
            <p>Number of seats: {seats}</p>
            <p>City: {city}</p>
            <p>Street: {street}</p>
            <p>Rental time: {rental.rentalTime}</p>
            <p>Return time: {rental.returnTime}</p>
            <p>Price: {rental.price}</p>
            
       
        <div>
            <button onClick={handleExtendRental}>Extend rental</button>
        </div>
        </div>
    );
};

export default ActivityFuture;
