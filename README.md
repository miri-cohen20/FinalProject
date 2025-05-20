# City Car - Vehicle Rental Management System

### Project Description
**City Car** is an advanced system for managing and renting vehicles, designed for customers, employees, and managers.  
The system offers a user-friendly and modern interface, allowing each user to perform actions tailored to their needs.  
Payment for vehicle rentals is based on time rates, according to the duration of the rental.

---

## Key Features
### Customers:
- Rent vehicles from various locations.
- Return vehicles at the end of the rental period (available only for active rentals).
- Report issues with vehicles (e.g., maintenance or cleanliness) – available only for active rentals.
- View rental history.
- Easily extend rental periods.
- Search for vehicles using filters:
  - Number of seats.
  - Location.
  - Availability.

### Employees:
- View vehicles that require maintenance, based on their role.
- Report vehicle repairs.
- View their monthly salary calculations.

### Managers:
- Add new vehicles to the system.
- Add new employees.
- Perform overall system management.

---

## Technologies
- **Backend**: Developed in `C#`.
- **Frontend**: User interface developed using `React.js`.
- **Database**: Managed using `SQL`.

---

## System Requirements
Before getting started, make sure your system meets the following requirements:
- **Node.js** (version 14 or higher): To run the client-side.
- **Visual Studio** with support for `.NET Framework`: To run the server-side.
- **SQL Server** installed: For database management.

---

## Steps to Run the System

### 1. Backend Setup
1. Open the project in **Visual Studio**.
2. Update the database connection string (`Connection String`) in the `appsettings.json` file:
   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "Server=your-server-name;Database=CityCarDB;Trusted_Connection=True;"
       }
   }
   ```
3. Run the migrations to create the database:
   ```powershell
   Update-Database
   ```
4. Start the server:
   - Click `Start` or use the shortcut `F5`.

---

### 2. Frontend Setup
1. Navigate to the client-side folder (React App) in the terminal:
   ```bash
   cd path/to/client
   ```
2. Install all dependencies:
   ```bash
   npm install
   ```
3. Start the local server:
   ```bash
   npm start
   ```
4. Open your browser and navigate to:
   ```
   http://localhost:3000
   ```

---

### 3. Database Setup
1. Ensure the `CityCarDB` database has been successfully created.
2. If there is seed data, run the appropriate code in Visual Studio.

---

## Project Structure
- **Backend**: All API and database management files are located in the server folder.
- **Frontend**: The client folder contains the user interface developed using React.
- **Database**: Migration files and models are located in the backend.

---

## Acknowledgments
Thank you to everyone who supported and contributed to the development of this project!

---

## Contributing
If you would like to add or improve functionality, please reach out to us or open a Pull Request.

---

## Authors
This project was developed by [Miri Cohen](https://github.com/miri-cohen20) and [Tami Weber](https://github.com/tami-weber).

