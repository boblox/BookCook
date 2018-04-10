import {Component, HostBinding, HostListener, Input, OnInit} from '@angular/core';
import {Observable} from 'rxjs/Observable';

// export class ConfirmDialogData {
//     title?: string | Observable<string>;
//     text?: string | Observable<string>;
//
//     public constructor(init?: Partial<ConfirmDialogData>) {
//         Object.assign(this, init);
//     }
// }

@Component({
    selector: 'app-confirm-dialog',
    templateUrl: './confirm-dialog.html',
    styleUrls: ['./confirm-dialog.scss']
})
export class ConfirmDialogComponent implements OnInit {
    @HostBinding('hidden') private hidden = true;
    @Input() title: string;
    @Input() cancelText: string;
    @Input() confirmText: string;
    // @Input() data: ConfirmDialogData;
    private cancel: () => void;
    private confirm: () => void;

    // buttonStyle: typeof ButtonStyle = ButtonStyle;

    constructor() {
    }

    @HostListener('document:keydown', ['$event'])
    onKeyUp(e: any) {
        if (e.keyCode === 27) {
            this.onCancel();
        } else if ([32, 13].indexOf(e.keyCode) >= 0) {
            e.preventDefault();
            this.onConfirm();
        }
    }

    ngOnInit() {
    }

    show(cancel: () => void, confirm: () => void/*, data?: ConfirmDialogData*/) {
        this.hidden = false;
        this.cancel = cancel;
        this.confirm = confirm;
        // this.data = data;
    }

    hide() {
        this.hidden = true;
    }

    onCancel() {
        this.cancel();
    }

    onConfirm() {
        this.confirm();
    }
}
