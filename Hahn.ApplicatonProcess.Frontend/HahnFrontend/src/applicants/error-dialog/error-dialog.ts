import { DialogController, DialogService } from 'aurelia-dialog';
import { autoinject } from 'aurelia-framework';

@autoinject
export class ErrorDialog {
  messages: string[] = [];
  constructor(private dialogController: DialogController) {
  }
  activate(errorMessages: string[]) {
    this.messages = errorMessages;
  }
  confirm() {
    this.dialogController.ok(true);
  }

  cancel() {
    this.dialogController.close(false, false);
  }
}
