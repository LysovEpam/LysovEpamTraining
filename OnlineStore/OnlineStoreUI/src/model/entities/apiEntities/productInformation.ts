import { ProductCategory } from './productCategory';

export class ProductInformation{
    idEntity: number;
    productName: string;
    imageLocalSource: string;
    description: string;
    productCategories: ProductCategory[];

    constructor(){
        this.idEntity = 0;
        this.productName = '';
        this.imageLocalSource = '';
        this.description = '';
        this.productCategories = [];
    }
	
}