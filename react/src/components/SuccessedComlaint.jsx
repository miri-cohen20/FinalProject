import React from 'react';
import { useNavigate } from 'react-router-dom';
import "./AppTheme.css";
const SuccessedComlaint = () => {
    const navigate = useNavigate();

    return (
        <div style={styles.container}>
            <div style={styles.icon}>✅</div>
            <h2 style={styles.title}>התלונה נשלחה בהצלחה!</h2>
            <p style={styles.text}>
                תודה שפנית אלינו, נטפל בפנייתך בהקדם האפשרי.
            </p>
            <div style={styles.buttons}>
                <button
                    style={styles.button}
                    onClick={() => navigate('/')}
                >
                    חזרה לדף הבית
                </button>
                <button
                    style={styles.buttonSecondary}
                    onClick={() => navigate('/ActivityRentals')}
                >
                    לצפייה בהשכרות הפעילות שלי
                </button>
            </div>
        </div>
    );
};

const styles = {
    container: {
        direction: "rtl",
        maxWidth: 400,
        margin: "70px auto",
        padding: "30px",
        borderRadius: "12px",
        boxShadow: "0 2px 12px rgba(0,0,0,0.1)",
        background: "#fff",
        textAlign: "center"
    },
    icon: {
        fontSize: "64px",
        marginBottom: "20px"
    },
    title: {
        margin: "0 0 12px 0",
        color: "#28a745"
    },
    text: {
        margin: "0 0 28px 0",
        color: "#444",
        fontSize: "18px"
    },
    buttons: {
        display: "flex",
        justifyContent: "center",
        gap: "12px"
    },
    button: {
        padding: "10px 20px",
        fontSize: "16px",
        background: "#28a745",
        color: "#fff",
        border: "none",
        borderRadius: "6px",
        cursor: "pointer"
    },
    buttonSecondary: {
        padding: "10px 20px",
        fontSize: "16px",
        background: "#f1f1f1",
        color: "#333",
        border: "none",
        borderRadius: "6px",
        cursor: "pointer"
    }
};

export default SuccessedComlaint;