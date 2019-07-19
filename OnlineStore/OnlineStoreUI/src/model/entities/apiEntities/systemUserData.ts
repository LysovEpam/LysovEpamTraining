import { ProductCategory } from './productCategory';

export class SystemUserData{
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    login: string;
    status: string;
    role: string;
    newPassword:string;
   
    constructor( firstName: string, lastName: string, email: string, phone: string, login: string, status: string, role: string, newPassword:string){
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phone = phone;
        this.login = login;
        this.status = status;
        this.role = role;
        this.newPassword = newPassword;
    }
	
}


