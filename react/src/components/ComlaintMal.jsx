import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate, useLocation } from 'react-router-dom';
import { fetchComlaintMalasyncAction } from '../redux/Thunk';
import "./AppTheme.css";
const ComlaintMal = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const location = useLocation();
    const { rental } = location.state || {};

    const [description, setDescription] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        if (!description.trim()) {
            setError('אנא הזן תיאור תקלה');
            return;
        }
        setLoading(true);
        try {
            const resultAction =  dispatch(fetchComlaintMalasyncAction({ rentalId: rental.id, descraption: description }));
            if (fetchComlaintMalasyncAction.fulfilled()) {
                navigate('/SuccessedComlaint');
            } else {
                setError(resultAction.payload || 'אירעה שגיאה בשליחת התלונה');
            }
        } catch (err) {
            setError('אירעה שגיאה בלתי צפויה');
        }
        setLoading(false);
    };

    return (
        <div style={styles.container}>
            <h2 style={styles.title}>תלונה על חוסר תקינות הרכב</h2>
            <form onSubmit={handleSubmit} style={styles.form}>
                <label style={styles.label}>
                    תיאור התקלה:
                    <textarea
                        style={styles.textarea}
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        placeholder="פרט/י את הבעיה ברכב..."
                        rows={5}
                        disabled={loading}
                    />
                </label>
                {error && <div style={styles.error}>{error}</div>}
                <button
                    type="submit"
                    style={styles.button}
                    disabled={loading}
                >
                    {loading ? 'שולח...' : 'שלח תלונה'}
                </button>
            </form>
        </div>
    );
};

const styles = {
    container: {
        maxWidth: 400,
        margin: '50px auto',
        padding: '32px',
        borderRadius: '10px',
        boxShadow: '0 2px 12px rgba(0,0,0,0.1)',
        background: '#fff',
        direction: 'rtl'
    },
    title: {
        marginBottom: '20px',
        textAlign: 'center',
        color: '#2c3e50'
    },
    form: {
        display: 'flex',
        flexDirection: 'column',
        gap: '16px'
    },
    label: {
        fontWeight: 'bold',
        marginBottom: '8px'
    },
    textarea: {
        width: '100%',
        padding: '10px',
        borderRadius: '6px',
        border: '1px solid #ccc',
        fontSize: '16px',
        marginTop: '8px',
        resize: 'vertical'
    },
    button: {
        padding: '12px',
        background: '#28a745',
        color: '#fff',
        border: 'none',
        borderRadius: '6px',
        fontSize: '16px',
        cursor: 'pointer'
    },
    error: {
        color: 'red',
        marginBottom: '8px',
        textAlign: 'center'
    }
};

export default ComlaintMal;






