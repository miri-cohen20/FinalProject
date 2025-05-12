
// export const fetchSighUp = async (customerData) => {
//     const response = await fetch("http://localhost:5108/api/Sigh/sighUpCustomer", {
//         method: 'POST',
//         headers: {
//             'Content-Type': 'application/json',
//         },
//         body: JSON.stringify(customerData),
//     });
//     return response;
// }


export const fetchSighUp = async (customerData) => {
    const response = await fetch("http://localhost:5108/api/Sigh/sighUpCustomer", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(customerData),
    });

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Request failed!');
    }

    return await response.json(); 
};






