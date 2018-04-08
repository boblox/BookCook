import { Component, OnInit, Input, HostBinding } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { RecipesService } from '../../services';
import { Recipe } from '../../models';

//export class ConfirmDialogData {
//    title?: string | Observable<string>;
//    text?: string | Observable<string>;

//    public constructor(init?: Partial<ConfirmDialogData>) {
//        Object.assign(this, init);
//    }
//}

export enum ManageMode {
    View,
    Edit
}

@Component({
    selector: 'app-manage-recipe',
    templateUrl: './manage-recipe.component.html',
    styleUrls: ['./manage-recipe.component.scss']
})
export class ManageRecipeComponent implements OnInit {
    @HostBinding('hidden') private hidden = true;
    @Input() title: string;
    @Input() cancelText: string;
    @Input() confirmText: string;
    //@Input() data: ConfirmDialogData;
    private recipe: Recipe;
    private mode: ManageMode;
    private cancel: () => void;
    private confirm: (recipe: Recipe) => void;

    constructor(private recipesService: RecipesService) {
    }

    ngOnInit() {
    }

    show(cancel: () => void,
        confirm: (recipe: Recipe) => void,
        recipe: Recipe,
        mode: ManageMode) {
        this.hidden = false;
        this.cancel = cancel;
        this.confirm = confirm;
        this.recipe = recipe;
        this.mode = mode;
        //TODO:don't store constants in code
        this.title = this.mode == ManageMode.View ?
            'View recipe' :
            recipe.id === 0 ? 'Create recipe' : 'Edit recipe';
    }

    hide() {
        this.hidden = true;
    }

    onCancel() {
        this.hide();
        this.cancel();
    }

    onConfirm() {
        this.recipesService.saveRecipe(this.recipe)
            .subscribe(data => {
                //TODO: do something!!
                //this.recipe.data = data;
            });
        this.confirm(this.recipe);
    }
}
