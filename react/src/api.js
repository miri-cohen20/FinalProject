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
export const fetchEndRental = async ({ customerId, rentalId }) => {
  try {
    const response = await fetch(`http://localhost:5108/api/RentalAvailable/endRental?idCustomer=${customerId}&idRenting=${rentalId}`, {
      method: 'PUT', // שינוי לשיטת PUT
      headers: {
        'Content-Type': 'application/json', // הגדרת סוג התוכן
      },
      body: JSON.stringify({ idCustomer: customerId, idRenting: rentalId }) // שליחת נתונים בגוף הבקשה
    });

    const contentType = response.headers.get("Content-Type");
    let responseData;

    if (contentType && contentType.includes("application/json")) {
      responseData = await response.json();
    } else {
      responseData = await response.text();
    }

    if (!response.ok) {
      throw new Error(responseData.message || responseData);
    }

    return responseData;
  } catch (error) {
    console.error('There has been a problem with your fetch operation:', error);
    return Promise.reject(error); // החזרת Promise.reject במקרה של שגיאה
  }
};

export const fetchComlaintMal = async ({rentalId,descraption }) => {
  try {
    const response = await fetch(`http://localhost:5108/api/RentalAvailable/improperty?idRenting=${rentalId}`, {
      method: 'PUT', // שינוי לשיטת PUT
      headers: {
        'Content-Type': 'application/json', // הגדרת סוג התוכן
      },
      body: JSON.stringify( descraption ) // שליחת נתונים בגוף הבקשה
    });

    const contentType = response.headers.get("Content-Type");
    let responseData;

    if (contentType && contentType.includes("application/json")) {
      responseData = await response.json();
    } else {
      responseData = await response.text();
    }

    if (!response.ok) {
      throw new Error(responseData.message || responseData);
    }

    return responseData;
  } catch (error) {
    console.error('There has been a problem with your fetch operation:', error);
    return Promise.reject(error); // החזרת Promise.reject במקרה של שגיאה
  }
};


export const fetchComlaintCleaning = async ({rentalId,descraption }) => {
  try {
    const response = await fetch(`http://localhost:5108/api/RentalAvailable/lackOfCleanliness?idRenting=${rentalId}`, {
      method: 'PUT', // שינוי לשיטת PUT
      headers: {
        'Content-Type': 'application/json', // הגדרת סוג התוכן
      },
      body: JSON.stringify( descraption ) // שליחת נתונים בגוף הבקשה
    });

    const contentType = response.headers.get("Content-Type");
    let responseData;

    if (contentType && contentType.includes("application/json")) {
      responseData = await response.json();
    } else {
      responseData = await response.text();
    }

    if (!response.ok) {
      throw new Error(responseData.message || responseData);
    }

    return responseData;
  } catch (error) {
    console.error('There has been a problem with your fetch operation:', error);
    return Promise.reject(error); // החזרת Promise.reject במקרה של שגיאה
  }
};




export const fetchGetUntilExtendRental = async ({ idCustomer, idRenting }) => {
  try {
    const response = await fetch(
      `http://localhost:5108/api/RentalAvailable/getUntilCanRental?idCustomer=${idCustomer}&idRenting=${idRenting}`
    );
    if (!response.ok) {
      throw new Error(response.statusText);
    }
let date = await response.json()
    const extendTimeString = await new Date(date).toISOString();
    console.log('Response from server:', extendTimeString);

    // מחזירים את המחרוזת בדיוק כמו שהתקבלה מהשרת
    return extendTimeString;
  } catch (error) {
    console.error('There has been a problem with your fetch operation:', error);
    return { error: error.message };
  }
};


export const fetchGetPriceForExtendRental = async ({ rentalId, customerId, untilTime }) => {
  try {
    const response = await fetch(
      `http://localhost:5108/api/RentalAvailable/GetPriceForExtendRental?idRenting=${rentalId}&customerId=${customerId}`,
      {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(untilTime)
      }
    );

    const responseText = await response.text();

    if (!response.ok) {
      // במצב של שגיאה בשרת, נחזיר את הודעת השגיאה שהגיעה מהשרת (responseText)
      throw new Error(responseText || 'שגיאה לא ידועה מהשרת');
    }

    const price = Number(responseText);
    if (isNaN(price)) {
      throw new Error('השרת לא החזיר מחיר תקני');
    }

    return price;
  } catch (error) {
    // error.message יכיל את הודעת השגיאה מהשרת (אם הייתה)
    console.error('There has been a problem with your fetch operation:', error);
    return Promise.reject(error); // חשוב להחזיר Promise.reject כדי שה-catch בקומפוננטה יעבוד
  }
};



export const fetchExtendRental = async ({rentalId,customerId, untilTime }) => {
  try {
    const response = await fetch(`http://localhost:5108/api/RentalAvailable/extendRental?idRenting=${rentalId}&customerId=${customerId}`, {
      method: 'PUT', // שינוי לשיטת PUT
      headers: {
        'Content-Type': 'application/json', // הגדרת סוג התוכן
      },
      body: JSON.stringify( untilTime ) // שליחת נתונים בגוף הבקשה
    });

    const contentType = response.headers.get("Content-Type");
    let responseData;

    // if (contentType && contentType.includes("application/json")) {
    //   responseData = await response.json();
    // } else {
    //   responseData = await response.text();
    // }

    if (!response.ok) {
      throw new Error(responseData.message || responseData);
    }

    return response;
  } catch (error) {
    console.error('There has been a problem with your fetch operation:', error);
    return Promise.reject(error); // החזרת Promise.reject במקרה של שגיאה
  }
};


