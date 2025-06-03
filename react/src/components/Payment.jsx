import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { fetchExtendRentalAsinc } from "../redux/Thunk";
import "./AppTheme.css";
const Payment = () => {
    const location = useLocation();
    const { rental, untilTime, price } = location.state;
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const handlePay = async () => {
        setLoading(true);
        setError('');
        try {
            // ביצוע הארכה בפועל (כולל תשלום)
            await dispatch(fetchExtendRentalAsinc({
                rentalId: rental.id,
                customerId: rental.idCustomer,
                untilTime
            })).unwrap();
            navigate("/extend-success", { state: { untilTime } });
        } catch {
            setError("שגיאה בתשלום או בהארכת ההשכרה");
        }
        setLoading(false);
    };

    return (
        <div style={styles.container}>
            <h2 style={styles.title}>לתשלום</h2>
            <p>סך הכל לתשלום עבור הארכה: <b>{price} ₪</b></p>
            <button onClick={handlePay} disabled={loading} style={styles.button}>
                {loading ? "מעבד תשלום..." : "שלם וסיים"}
            </button>
            {error && <p style={styles.error}>{error}</p>}
        </div>
    );
};

const styles = { /* ... (אותו קוד עיצוב שלך) ... */ };

export default Payment;