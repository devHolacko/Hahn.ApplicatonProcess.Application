import { DialogController } from 'aurelia-dialog';
import { autoinject } from 'aurelia-framework';

@autoinject
export class ResetDialog {

  constructor(private dialogController: DialogController) {
  }

  confirm() {
    this.dialogController.ok(true);
  }

  cancel() {
    this.dialogController.close(false, false);
  }
}
