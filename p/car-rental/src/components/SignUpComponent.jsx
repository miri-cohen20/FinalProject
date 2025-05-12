
import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchSighUpAsyncAction } from "../redux/Thunk";

const SignUpComponent = () => {
    const dispatch = useDispatch();
    const loading = useSelector((state) => state.sigh.loading);
    const error = useSelector((state) => state.sigh.error);
    
    const [customerData, setCustomerData] = useState({
        firstName: '',
        lastName: '',
        phoneNumber: '',
        email: '',
        city: '',
        street: '',
        buildingNumber: '',
        password: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCustomerData({ ...customerData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const action = await dispatch(fetchSighUpAsyncAction(customerData));
        if (fetchSighUpAsyncAction.fulfilled.match(action)) {
            alert('Registration successful:', action.payload);
        } else {
           alert('Registration failed:', action.error.message);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input name="firstName" placeholder="First Name" onChange={handleChange} required />
            <input name="lastName" placeholder="Last Name" onChange={handleChange} />
            <input name="phoneNumber" placeholder="Phone Number" required onChange={handleChange} />
            <input name="email" placeholder="Email" type="email" required onChange={handleChange} />
            <input name="city" placeholder="City" required onChange={handleChange} />
            <input name="street" placeholder="Street" required onChange={handleChange} />
            <input name="buildingNumber" placeholder="Building Number" onChange={handleChange} />
            <input name="password" placeholder="Password" type="password" required onChange={handleChange} />
            <button type="submit" disabled={loading}>
                {loading ? "Registering..." : "Sign Up"}
            </button>
            {error && <p style={{ color: 'red' }}>{error}</p>}
        </form>
    );
};

export default SignUpComponent;

