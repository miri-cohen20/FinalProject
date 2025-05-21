export const fetchSighUp = async (customerData) => {
    const response = await fetch("http://localhost:5108/api/Sigh/sighUpCustomer", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(customerData),
    });
    const contentType = response.headers.get("Content-Type");
    let responseData;
    if (contentType && contentType.includes("application/json")) {
        responseData = await response.json();
    } else {
        responseData = await response.text();
    }
    if (!response.ok) {
        throw new Error(responseData.message || responseData || 'Request failed!');
    }
    return responseData;
};

export const fetchLogin = async (loginData) => {
    const response = await fetch("http://localhost:5108/api/Sigh/login", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            id: loginData.id,
            password: loginData.password
        }),
    });
    const contentType = response.headers.get("Content-Type");
    let responseData;
    if (contentType && contentType.includes("application/json")) {
        responseData = await response.json();
    } else {
        responseData = await response.text();
    }
    if (!response.ok) {
        throw new Error(responseData.message || responseData || 'Login failed!');
    }
    return responseData;
};


// עדכון פרטי לקוח
export const updateCustomerDetailsApi = async (id, data) => {
    const response = await fetch(`http://localhost:5108/api/Customer/updateCustomer?id=${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    });
    const contentType = response.headers.get("Content-Type");
    let responseData;
    if (contentType && contentType.includes("application/json")) {
        responseData = await response.json();
    } else {
        responseData = await response.text();
    }
    if (!response.ok) {
        throw new Error(responseData.message || responseData || "עדכון נכשל");
    }
    return responseData;
};

// שליפת היסטוריית השכרות
export const fetchRentalHistoryApi = async (customerId) => {
    const response = await fetch(`http://localhost:5108/api/Customer/getAllMyCurrentRentals?idCustomer=${customerId}`);
    if (!response.ok) throw new Error("שגיאה בשליפת היסטוריה");
    return await response.json();
};
export const fetchAllCar = async () => {
    const response = await fetch("http://localhost:5108/api/Customer/getAllCar", {
        method: 'GET',
        
    });

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    return await response.json(); 
};
export const fetchAllRenting = async () => {
    const response = await fetch("http://localhost:5108/api/Customer/getAllRenting", {
        method: 'GET',
        
    });

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message);
    }

    return await response.json(); 
};