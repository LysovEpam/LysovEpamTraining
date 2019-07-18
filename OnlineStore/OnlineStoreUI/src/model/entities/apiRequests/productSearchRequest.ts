export class ProductSearchRequest{
    minCost: number;
    maxCost: number;
    productSearch: string;
    productStatuses:string[];
    idProductCategories: number[];

    constructor(minCost: number,maxCost: 
        number,productSearch: string, 
        productStatuses:string[],
        idProductCategories: number[]){

        this.minCost = minCost;
        this.maxCost = maxCost;
        this.productSearch = productSearch;
        this.productStatuses = productStatuses;
        this.idProductCategories = idProductCategories;
    }
}
