import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {RecipesService} from '../../services';
import {Recipe, RecipeData} from '../../models';
import {ManageRecipeComponent, ManageMode} from '../manage-recipe/manage-recipe.component';
import {ConfirmDialogComponent} from '../confirm-dialog/confirm-dialog.component';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
    readonly maxFieldLength = 60;
    public recipes: Recipe[];
    public dialogText = {};
    @ViewChild(ManageRecipeComponent) manageRecipeDialog: ManageRecipeComponent;
    @ViewChild(ConfirmDialogComponent) confirmDeleteDialog: ConfirmDialogComponent;

    constructor(private recipesService: RecipesService) {
        this.dialogText = {
            cancel: 'Cancel',
            save: 'Save',
            confirm: 'Confirm',
            confirmDeleteTitle: 'Delete confirmation',
            confirmDeleteBody: 'Are you sure you want to delete this recipe?'
        };
    }

    private static sortRecipes(recipes: Recipe[]) {
        recipes.sort((a, b) =>
            b.dateCreated.getTime() - a.dateCreated.getTime()
        );
    }

    ngOnInit(): void {
        this.recipesService.getRecipes()
            .subscribe(data => {
                HomeComponent.sortRecipes(this.recipes = data);
            });
    }

    addRecipe() {
        let newRecipe = new Recipe({
            id: 0,
            data: new RecipeData()
        });
        this.manageRecipeDialog.show(
            () => {
            },
            (createdRecipe: Recipe) => {
                this.recipes.push(createdRecipe);
                HomeComponent.sortRecipes(this.recipes);
            },
            newRecipe,
            ManageMode.Edit);
    }

    editRecipe(recipe: Recipe) {
        let recipeClone = JSON.parse(JSON.stringify(recipe));
        this.manageRecipeDialog.show(
            () => {
            },
            (updatedRecipe: Recipe) => {
                recipe.data = updatedRecipe.data;
            },
            recipeClone,
            ManageMode.Edit);
    }

    deleteRecipe(recipe: Recipe) {
        this.confirmDeleteDialog.show(
            () => {
                this.confirmDeleteDialog.hide();
            },
            () => {
                this.recipesService.deleteRecipe(recipe.id)
                    .subscribe(
                        (success: boolean) => {
                            this.confirmDeleteDialog.hide();
                            if (success) {
                                let index = this.recipes.indexOf(recipe);
                                this.recipes.splice(index, 1);
                            } else {
                                // TODO: show meaningful error to user!
                            }
                        },
                        (error) => {
                            // TODO: show meaningful error to user!
                            this.confirmDeleteDialog.hide();
                        });
            }
        );
    }

    getShortName(s: string) {
        return s && s.length > this.maxFieldLength ? s.substring(0, this.maxFieldLength) + '...' : s;
    }
}
