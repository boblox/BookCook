import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {RecipesService} from '../../services';
import {Recipe, RecipeData} from '../../models';
import {ManageRecipeComponent, ManageMode} from '../manage-recipe/manage-recipe.component';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
    public recipes: Recipe[];
    @ViewChild(ManageRecipeComponent) manageRecipeDialog: ManageRecipeComponent;

    constructor(private recipesService: RecipesService) {

    }

    ngOnInit(): void {
        this.recipesService.getRecipes()
            .subscribe(data => {
                this.recipes = data
                    .sort((a, b) =>
                        b.dateCreated.getTime() - a.dateCreated.getTime()
                    );
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
        this.recipesService.deleteRecipe(recipe.id)
            .subscribe((success: boolean) => {
                if (success) {
                    let index = this.recipes.indexOf(recipe);
                    this.recipes.splice(index, 1);
                }
            });
    }
}
