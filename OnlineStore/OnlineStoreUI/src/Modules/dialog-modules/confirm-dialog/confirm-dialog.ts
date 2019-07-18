import {Component, Inject} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

export class DialogData {
  caption: string;
  message: string;

  constructor(caption: string, message: string){
    this.caption = caption;
    this.message = message;
  }
}


@Component({
  selector: 'confirm-dialog',
  templateUrl: 'confirm-dialog.html',
})
export class ConfirmDialog {

  constructor(
    public dialogRef: MatDialogRef<ConfirmDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

    cancelClick(): void {
      this.dialogRef.close(false);}

    okClick(): void {
      this.dialogRef.close(true);}


  

}