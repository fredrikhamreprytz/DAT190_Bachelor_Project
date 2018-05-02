using System;
using DAT190_Bachelor_Project.Model;
namespace DAT190_Bachelor_Project.Data
{
    public interface IDBCom
    {

        // Full CRUD pattern for User class
        bool AddUser(User user);
        User FetchUser(string Id);
        bool UpdateUser(User user);
        bool DeleteUser(User user);

        // A user must always have a footprint.
        // Create and delete are therefore not necessary.
        // A footprint object is created and stored in DB by WebService when a user is created
        // WebService is responsible for updating the CarbonFootprint object when one of its Emission 
        // objects are updated.
        CarbonFootprint FetchFootprint(User user);

        // Emission is a presenter class and does not need create, update or delete methods
        // It is necessary for the CarbonFootprint class, which is responsible for creating the initial Emission obejcts.
        // WebService is responsible for updating the Emission object when one of its DataSources is updated or added.
        IEmission FetchFlightEmissions(CarbonFootprint carbonFootprint);
        IEmission FetchFuelEmissions(CarbonFootprint carbonFootprint);
        IEmission FetchHouseholdEmissions(CarbonFootprint carbonFootprint);

        // Full CRUD pattern for DataSource
        // any add, update or delete operations on a DataSource object should
        // prompt WebService to update the Emission object that contains the DataSource.
        // Updating the Emission object should in turn prompt the WebService to update
        // the CarbonFootprint object that contains the Emission
        // This way we do not need to fetch Lists of DataSource object before they are
        // actually needed (Emission detail views).
        bool AddDataSource(DataSource dataSource);
        DataSource[] FetchDataSources(IEmission emission);
        bool UpdateDataSource(DataSource dataSource);
        bool DeleteDataSource(DataSource dataSource);

        // Full CRUD pattern for Vehicle class
        bool AddVehicle(Vehicle vehicle);
        bool UpdateVehicle(Vehicle vehicle);
        Vehicle[] FetchVehicles(User user);
        bool DeleteVehicle(Vehicle vehicle);


    }
}
