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
    const preparedData = {
      ...data,
      id: id, // חובה!
      buildingNumber:
        data.buildingNumber === "" || data.buildingNumber === null
          ? null
          : Number(data.buildingNumber)
    };
    const response = await fetch(`http://localhost:5108/api/Customer/updateCustomer?id=${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(preparedData)
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

export const fetchAllRenting = async () => {
  const response = await fetch(`http://localhost:5108/api/Customer/getAllRenting`);
  if (!response.ok) throw new Error(response.message);
  return await response.value.json();
};
//  export const  fetchAllCar = async()=> {
//   try {
//       const response = await fetch(`http://localhost:5108/api/Customer/getAllCar`);
//       if (!response.ok) {
//           throw new Error(response.message);
//       }
//       const data = await response.json(); // כאן אתה מקבל את ה-JSON
//       const cars = data.$values; // גישה למערך הרכבים
//       console.log(cars); // הדפס את הרכבים
//   } catch (error) {
//       console.error('There has been a problem with your fetch operation:', error);
//   }
//   return cars
// };


export const fetchAllCar = async () => {
  try {
    const response = await fetch(`http://localhost:5108/api/Customer/getAllCar`);
    if (!response.ok) {
      throw new Error(response.statusText);
    }
    const data = await response.json(); // כאן אתה מקבל את ה-JSON
    const cars = data.$values.map(item => ({
      id: item.id,
      seats: item.seats,
      cleanStatus: item.cleanStatus,
      properStatus: item.properStatus,
      finalCleaning: new Date(item.finalCleaning).toISOString(), // Convert to string
      lastCorrection: new Date(item.lastCorrection).toISOString(), // Convert to string
      
        city: item.city,
        street: item.street,
      
      
        cleaning: item.descriptionCleaning,
        proper: item.descriptionProper,
      
      rentings: item.rentings.$values,
    }));
    console.log(cars); // הדפס את הרכבים
    return cars; // החזרת הרשימה של האובייקטים
  } catch (error) {
    console.error('There has been a problem with your fetch operation:', error);
    return []; // החזרת מערך ריק במקרה של שגיאה
  }
}



export const fetchAllActivityRentals = async (idCustomer) => {
  try {
    const response = await fetch(`http://localhost:5108/api/Customer/getAllMyCurrentRentals?idCustomer=${idCustomer}`);
    if (!response.ok) {
      throw new Error(response.statusText);
    }
    const data = await response.json(); // כאן אתה מקבל את ה-JSON
    const rentals = data.$values.map(item => ({
      id: item.id,
      idCar: item.idCar,
      idCustomer: item.idCustomer,
      rentalTime: new Date(item.rentalTime).toISOString(),
      returnTime: new Date(item.returnTime).toISOString(),
      price: item.price,
      available: item.available
    })); // המרה לאובייקטים רגילים
    console.log(rentals); // הדפס את הרכבים
    return rentals; // החזרת הרשימה של האובייקטים
  } catch (error) {
    console.error('There has been a problem with your fetch operation:', error);
    return []; // החזרת מערך ריק במקרה של שגיאה
  }
}


export const fetchAllActiveAndFutureRentals = async (idCustomer) => {
  try {
    const response = await fetch(`http://localhost:5108/api/Customer/GetMyActiveAndFutureRentals?idCustomer=${idCustomer}`);
    if (!response.ok) {
      throw new Error(response.statusText);
    }
    const data = await response.json(); // כאן אתה מקבל את ה-JSON
    const rentals = data.$values.map(item => ({
      id: item.id,
      idCar: item.idCar,
      idCustomer: item.idCustomer,
      rentalTime: new Date(item.rentalTime).toISOString(),
      returnTime: new Date(item.returnTime).toISOString(),
      price: item.price,
      available: item.available
    })); // המרה לאובייקטים רגילים
    console.log(rentals); // הדפס את הרכבים
    return rentals; // החזרת הרשימה של האובייקטים
  } catch (error) {
    console.error('There has been a problem with your fetch operation:', error);
    return []; // החזרת מערך ריק במקרה של שגיאה
  }
}