import {Component, Inject} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Product } from 'src/model/entities/apiEntities/product';
import { ProductStatus, ProductStatusEnum } from 'src/model/entities/apiEntities/additional/productStatus';


@Component({
  selector: 'product-cart',
  templateUrl: 'product-cart.html',
  styleUrls: ['product-cart.css']
})
export class ProductCartDialog {

  price:number;
  status:string;
  productName:string;
  description:string;
  imageLocalSource:string;

  constructor(
    public dialogRef: MatDialogRef<ProductCartDialog>,
    @Inject(MAT_DIALOG_DATA) public product: Product) {


      this.price = product.price;
      this.status = ProductStatus.getStatusPrint(product.productStatus.status);
      this.productName = product.productInformation.productName;
      this.description = product.productInformation.description;
      this.imageLocalSource = product.productInformation.imageLocalSource;
    }

  
    closeClick(): void {
      this.dialogRef.close(true);}
}