import {Component, Inject} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Router } from '@angular/router';

export class CategoryDialogData {
  caption: string;
  message: string;
  idCategory:number;

  constructor(caption: string, message: string, idCategory:number){
    this.caption = caption;
    this.message = message;
    this.idCategory = idCategory;
  }
}


@Component({
  selector: 'category-dialog',
  templateUrl: 'category-dialog.html',
})
export class CategoryDialog {

  constructor(
    public dialogRef: MatDialogRef<CategoryDialog>,
    @Inject(MAT_DIALOG_DATA) public data: CategoryDialogData, private router: Router) {}

    closeClick(): void {
      this.dialogRef.close(false);}

      navigateClick(){

        this.router.navigate(['/products'], {
            queryParams:{
              'action': 'selectFilter',
              'minCost': 0,
              'maxCost': 0,
              'productSearch': '',
              'productStatus': '',
              'idProductCategory': [this.data.idCategory],
            }
          });

          this.dialogRef.close(true);

    }


  

}