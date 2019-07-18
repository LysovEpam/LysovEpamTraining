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
  selector: 'message-dialog',
  templateUrl: 'message-dialog.html',
})
export class MessageDialog {

  constructor(
    public dialogRef: MatDialogRef<MessageDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  
    okClick(): void {
      this.dialogRef.close(true);}


  

}