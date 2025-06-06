import { useNavigate } from "react-router-dom";
import "./AppTheme.css";
const SuccessedEnd = () => {
    const navigate = useNavigate();

    return (
        <div style={styles.container}>
            <div style={styles.icon}>🎉</div>
            <h2 style={styles.title}>ההשכרה הסתיימה בהצלחה!</h2>
            <p style={styles.text}>תודה שבחרת ב-City Car</p>
            <div style={styles.buttons}>
                <button style={styles.button} onClick={() => navigate("/")}>
                    לדף הבית
                </button>
                <button style={styles.buttonSecondary} onClick={() => navigate("/ActivityRentals")}>
                    להשכרות הפעילות שלי
                </button>
            </div>
        </div>
    );
};

const styles = {
    container: {
        direction: "rtl",
        maxWidth: 400,
        margin: "80px auto",
        padding: 32,
        borderRadius: 12,
        boxShadow: "0 2px 12px rgba(0,0,0,0.1)",
        background: "#fff",
        textAlign: "center",
    },
    icon: {
        fontSize: 56,
        marginBottom: 16,
    },
    title: {
        color: "#28a745",
        margin: "0 0 8px 0",
    },
    text: {
        color: "#444",
        margin: "0 0 24px 0",
        fontSize: "18px",
    },
    buttons: {
        display: "flex",
        justifyContent: "center",
        gap: "12px",
        marginTop: "24px"
    },
    button: {
        padding: "12px 24px",
        fontSize: "16px",
        background: "#28a745",
        color: "#fff",
        border: "none",
        borderRadius: "6px",
        cursor: "pointer",
        fontWeight: "bold",
    },
    buttonSecondary: {
        padding: "12px 24px",
        fontSize: "16px",
        background: "#e9ecef",
        color: "#333",
        border: "none",
        borderRadius: "6px",
        cursor: "pointer",
        fontWeight: "bold",
    }
};

export default SuccessedEnd;