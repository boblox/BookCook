import {Component, OnInit, Input, HostBinding, ViewChild} from '@angular/core';
import {RecipesService} from '../../services';
import {Recipe, RecipeData} from '../../models';
import {NgForm} from '@angular/forms';

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
    @ViewChild('recipeForm') recipeForm: NgForm;

    constructor(private recipesService: RecipesService) {
        this.recipe = new Recipe({data: new RecipeData()});
    }

    ngOnInit() {
    }

    show(cancel: () => void,
         confirm: (recipe: Recipe) => void,
         recipe: Recipe,
         mode: ManageMode) {
        let form = this.recipeForm.form;
        this.hidden = false;
        this.cancel = cancel;
        this.confirm = confirm;
        this.recipe = recipe;
        this.mode = mode;
        //TODO:don't store constants in code
        this.title = this.mode === ManageMode.View ?
            'View recipe' :
            recipe.id === 0 ? 'Create recipe' : 'Edit recipe';
        this.mode === ManageMode.View ?
            form.disable() : form.enable();
        form.markAsPristine();
    }

    hide() {
        this.hidden = true;
    }

    onCancel() {
        this.hide();
        this.cancel();
    }

    onConfirm() {
        if (this.recipeForm.valid) {
            this.recipesService.saveRecipe(this.recipe)
                .subscribe(data => {
                    this.hide();
                    this.confirm(data);
                });
        }
    }
}
