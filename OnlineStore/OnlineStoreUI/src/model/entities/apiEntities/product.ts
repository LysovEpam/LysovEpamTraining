import { ProductInformation } from './productInformation';
import { ProductStatus } from './additional/productStatus';

export class Product{
    idEntity: number;
    price:number;
    idProductInformation:number;
    productStatus:ProductStatus;
    productInformation: ProductInformation; 


    constructor(){
        this.idEntity = 0;
        this.price = 0;
        this.idProductInformation = 0;
        this.productStatus = null;
        this.productInformation = new ProductInformation();
    }
}