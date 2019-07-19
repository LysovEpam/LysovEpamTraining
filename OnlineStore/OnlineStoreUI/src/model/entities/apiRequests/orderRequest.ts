export class OrderRequest{
    idOrder: number;
    userLogin: string;
    idProducts: number[];
    address: string;
    status: string; 

    constructor(idOrder: number, userLogin: string, idProducts: number[], address: string, status: string){
        this.idOrder = idOrder;
        this.userLogin = userLogin;
        this.idProducts = idProducts;
        this.address = address;
        this.status = status;
    }
}




