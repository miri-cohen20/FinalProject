import { useLocation } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";


const ActivityFuture = () => {
    const location = useLocation();
    const { rental, seats, street,city } = location.state || {}; // קבלת פרטי ההשכרה
  

    if (!rental) {
        return <div>No rental details available.</div>; // טיפול במקרה שאין פרטים
    }
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
            
        </div>
    );
};

export default ActivityFuture;
