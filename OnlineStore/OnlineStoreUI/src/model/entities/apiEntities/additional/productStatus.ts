export class ProductStatus {
    status:ProductStatusEnum;

    constructor(status:ProductStatusEnum){
        this.status = status;
    }

    static getStatusPrint(status:ProductStatusEnum){

        if(status == ProductStatusEnum.Available)
            return 'Product available';
        if(status == ProductStatusEnum.NeedToOrder)
            return 'Product need to order';
        if(status == ProductStatusEnum.NotAvailable)
            return 'Product not available';
        
        return '';
    }

    getStatusPrint(){

        if(this.status == ProductStatusEnum.Available)
            return 'Product available';
        if(this.status == ProductStatusEnum.NeedToOrder)
            return 'Product need to order';
        if(this.status == ProductStatusEnum.NotAvailable)
            return 'Product not available';
        
        return '';
    }

    static getAllProdcutStatus():ProductStatus[]{

        let allStatus: ProductStatus[] = [
            new ProductStatus(ProductStatusEnum.Available),
            new ProductStatus(ProductStatusEnum.NeedToOrder),
            new ProductStatus(ProductStatusEnum.NotAvailable),
        ]

        return allStatus;
    }


}

export enum ProductStatusEnum {
    Available = 10,
	NeedToOrder = 11,
	NotAvailable = 12
}