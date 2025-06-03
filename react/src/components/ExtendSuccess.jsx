import React from "react";
import { useLocation, useNavigate } from "react-router-dom";
import "./AppTheme.css";
const ExtendSuccess = () => {
    const location = useLocation();
    const { untilTime } = location.state || {};
    const navigate = useNavigate();

    return (
        <div style={{
            direction: "rtl", maxWidth: 400, margin: "80px auto", padding: "32px",
            borderRadius: "10px", boxShadow: "0 2px 12px rgba(0,0,0,0.1)", background: "#e8f5e9", textAlign: "center"
        }}>
            <div style={{ fontSize: "56px", color: "#4caf50" }}>✓</div>
            <h2 style={{ color: "#388e3c" }}>הארכת ההשכרה בוצעה בהצלחה!</h2>
            {untilTime &&
                <p>ההשכרה הוארכה עד <b>{new Date(untilTime).toLocaleString('he-IL')}</b></p>
            }
            <button onClick={() => navigate("/ActivityRentals")} style={{
                marginTop: "16px", padding: "10px 16px", background: "#4caf50",
                color: "#fff", border: "none", borderRadius: "6px", fontSize: "16px", cursor: "pointer"
            }}>
                מעבר לרשימת ההשכרות שלי
            </button>
        </div>
    );
};

export default ExtendSuccess;