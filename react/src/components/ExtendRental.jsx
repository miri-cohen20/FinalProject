import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate, useLocation } from 'react-router-dom';
import { fetchUntilExtendRental, fetchGetPriceExtendRental } from "../redux/Thunk";
import "./AppTheme.css";
const ExtendRental = () => {
    const location = useLocation();
    const { rental } = location.state;
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const [maxExtendDate, setMaxExtendDate] = useState('');
    const [selectedDate, setSelectedDate] = useState('');
    const [price, setPrice] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const [priceLoading, setPriceLoading] = useState(false);

    useEffect(() => {
        const fetchMaxDate = async () => {
            try {
                setLoading(true);
                const res = await dispatch(fetchUntilExtendRental({
                    idCustomer: rental.idCustomer,
                    idRenting: rental.id
                }));
                setMaxExtendDate(res.payload);
            } catch {
                setError('שגיאה בשליפת תאריך הארכה מרבי');
            }
            setLoading(false);
        };
        fetchMaxDate();
    }, [dispatch, rental.idCustomer, rental.id]);

    const handleDateChange = async (e) => {
        const newDate = e.target.value;
        setSelectedDate(newDate);
        setPrice(null);
        setError('');
        if (!newDate) return;
        setPriceLoading(true);
        try {
            const res = await dispatch(fetchGetPriceExtendRental({
                rentalId: rental.id,
                customerId: rental.idCustomer,
                untilTime: newDate
            }));
    
            // בדיקה אם יש שגיאה ב-res (thunk)
            if (res.error) {
                setError(res.error.message || "שגיאה בשליפת מחיר מהשרת");
                setSelectedDate(""); // איפוס התאריך
                setPriceLoading(false);
                return;
            }
    
            // לפעמים השרת מחזיר שדה error בתוך ה-payload
            if (res.payload && res.payload.error) {
                setError(res.payload.error || "שגיאה בשליפת מחיר מהשרת");
                setSelectedDate(""); // איפוס התאריך
                setPriceLoading(false);
                return;
            }
    
            setPrice(res.payload);
        } catch (err) {
            setError(err.message || 'שגיאה בשליפת מחיר');
            setSelectedDate(""); // איפוס התאריך
        }
        setPriceLoading(false);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (!selectedDate || price === null) return;
        navigate("/payment", {
            state: {
                rental,
                untilTime: selectedDate,
                price: price
            }
        });
    };

    return (
        <div style={styles.page}>
            <div style={styles.card}>
                <div style={styles.ribbon}>חדש!</div>
                <h2 style={styles.title}>
                    <span role="img" aria-label="clock" style={{ fontSize: 32, verticalAlign: 'middle' }}>⏰</span>
                    הארכת השכרה
                </h2>
                {loading && <Loader />}
                {error && <p style={styles.error}>{error}</p>}
                {!loading && maxExtendDate && (
                    <form onSubmit={handleSubmit} style={styles.form}>
                        <label style={styles.label}>
                            עד מתי ניתן להאריך:
                            <span style={styles.dateBox}>{maxExtendDate.replace("T", " ").slice(0, 16)}</span>
                        </label>
                        <label style={styles.label}>
                            בחר/י תאריך ושעה לסיום החדש:
                            <input
                                type="datetime-local"
                                min={rental.returnTime ? new Date(rental.returnTime
                                    ).toISOString().slice(0, 16) : ""}
                                max={maxExtendDate ? new Date(maxExtendDate).toISOString().slice(0, 16) : ""}
                                value={selectedDate}
                                onChange={handleDateChange}
                                required
                                style={styles.input}
                                disabled={loading}
                                />
                            </label>
                        {priceLoading && <Loader text="בודק מחיר..." />}
                        {selectedDate && price !== null && (
                            <div style={styles.priceBox}>
                                <span style={styles.priceIcon}>💳</span>
                                מחיר ההארכה:
                                <b style={styles.price}>{price} ₪</b>
                            </div>
                        )}
                        <button
                            type="submit"
                            style={{
                                ...styles.button,
                                ...(loading || !selectedDate || price === null ? styles.buttonDisabled : {})
                            }}
                            disabled={loading || !selectedDate || price === null}
                        >
                            <span>אשר הארכה ועבור לתשלום</span>
                            <span style={{ marginRight: 8, fontWeight: 'bold' }}>→</span>
                        </button>
                    </form>
                )}
            </div>
        </div>
    );
};

// Loader component for animation
const Loader = ({ text }) => (
    <div style={styles.loaderWrap}>
        <div style={styles.loader}></div>
        {text && <span style={{ marginRight: 10 }}>{text}</span>}
    </div>
);

const styles = {
    page: {
        minHeight: "100vh",
        background: "linear-gradient(120deg, #e0c3fc 0%, #8ec5fc 100%)",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        direction: "rtl"
    },
    card: {
        position: "relative",
        width: "100%",
        maxWidth: 430,
        background: "#fff",
        borderRadius: "18px",
        boxShadow: "0 4px 32px 0 rgba(140, 148, 251,0.20)",
        padding: "36px 30px 28px 30px",
        margin: "30px 0",
        textAlign: "center",
        overflow: "hidden"
    },
    ribbon: {
        position: "absolute",
        top: "-14px",
        left: "-34px",
        background: "linear-gradient(90deg,#6a82fb,#fc5c7d)",
        color: "#fff",
        fontWeight: "bold",
        fontSize: "17px",
        padding: "8px 42px",
        transform: "rotate(-20deg)",
        boxShadow: "0 2px 8px rgba(140, 148, 251,0.15)",
        zIndex: 1
    },
    title: {
        marginBottom: "24px",
        color: "#6a82fb",
        fontWeight: 900,
        letterSpacing: ".5px",
        fontSize: "2em",
        textShadow: "0 2px 12px rgba(140, 148, 251,0.15)"
    },
    form: {
        display: "flex",
        flexDirection: "column",
        gap: "24px",
        alignItems: "stretch"
    },
    label: {
        fontWeight: 500,
        marginBottom: "8px",
        color: "#333",
        textAlign: "right",
        fontSize: "1.08em"
    },
    dateBox: {
        display: "inline-block",
        background: "#f3f3fc",
        color: "#6a82fb",
        fontWeight: "bold",
        borderRadius: "8px",
        padding: "4px 12px",
        marginRight: "10px",
        fontSize: "1.03em",
        boxShadow: "0 1px 3px rgba(140,148,251,0.11)"
    },
    input: {
        width: '100%',
        padding: '11px',
        borderRadius: '8px',
        border: '1.5px solid #bdbdfc',
        fontSize: '1em',
        marginTop: '10px',
        background: "#f8f8ff",
        transition: "border-color 0.2s",
        outline: "none"
    },
    priceBox: {
        background: "linear-gradient(90deg, #f3e7e9 0%, #e3eeff 100%)",
        borderRadius: "10px",
        padding: "13px 0",
        color: "#4a4a4a",
        fontWeight: "bold",
        fontSize: "1.1em",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        gap: "9px",
        margin: "8px 0 2px 0"
    },
    priceIcon: {
        fontSize: 23,
        marginLeft: 7,
        verticalAlign: "middle"
    },
    price: {
        color: "#fc5c7d",
        fontWeight: "bolder",
        fontSize: "1.09em",
        marginRight: 4
    },
    button: {
        marginTop: "12px",
        padding: "13px",
        background: "linear-gradient(90deg, #6a82fb 0%, #fc5c7d 100%)",
        color: "#fff",
        border: "none",
        borderRadius: "9px",
        fontSize: "1.17em",
        fontWeight: "bold",
        cursor: "pointer",
        boxShadow: "0 2px 18px 0 rgba(140, 148, 251,0.14)",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        transition: "background 0.2s"
    },
    buttonDisabled: {
        opacity: 0.45,
        cursor: "not-allowed",
        filter: "grayscale(30%)"
    },
    error: {
        color: "#fc5c7d",
        fontWeight: "bold",
        background: "#fff5f5",
        borderRadius: "7px",
        padding: "9px 0",
        margin: "12px 0",
        fontSize: "1.07em",
        boxShadow: "0 2px 10px 0 rgba(252,92,125,0.06)"
    },
    loaderWrap: {
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        gap: "12px",
        margin: "20px 0"
    },
    loader: {
        width: 26,
        height: 26,
        border: "3px solid #e0c3fc",
        borderTop: "3px solid #fc5c7d",
        borderRadius: "50%",
        animation: "spin 1s linear infinite"
    }
};

// Spinner animation
const styleSheet = document.createElement("style");
styleSheet.innerText = `
@keyframes spin { 100% { transform: rotate(360deg); } }
`;
document.head.appendChild(styleSheet);

export default ExtendRental;