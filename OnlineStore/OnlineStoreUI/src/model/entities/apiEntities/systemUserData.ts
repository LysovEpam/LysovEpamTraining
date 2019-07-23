export class SystemUserData{
    idUser:number;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    login: string;
    status: string;
    role: string;
    password:string;
    newPassword:string;

    constructor( idUser:number,
        firstName: string, lastName: string, 
        email: string, phone: string, 
        status: string, role: string,
        login: string,  password:string, newPassword:string){
        
        this.idUser = idUser;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phone = phone;
        this.login = login;
        this.status = status;
        this.role = role;
        this.password = password;
        this.newPassword = newPassword;
    }
	
}


