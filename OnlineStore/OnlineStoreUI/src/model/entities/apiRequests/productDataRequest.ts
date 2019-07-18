export class ProductDataRequest{
    idEntity:number;
    price: number;
    status: string;
    idProductInformation: number;

    constructor(idEntity:number, price: number, status: string, idProductInformation: number){
        this.idEntity = idEntity;
        this.price = price;
        this.status = status;
        this.idProductInformation = idProductInformation;
    }
}