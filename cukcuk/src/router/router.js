import Employee from '../views/employee/EmployeeList'
import Customer from '../views/customer/CustomerList'
export const routes = [
    {path : "/", name : "Employee", component : Employee},
    {path : "/customer", name : "Customer", component : Customer}
]