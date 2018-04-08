import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SettingsService, RecipesService } from '../../services';
import { Recipe } from '../../models';
import { ManageRecipeComponent, ManageMode } from '../manage-recipe/manage-recipe.component';

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
                this.recipes = data;
            });
    }

    addRecipe() {
        this.manageRecipeDialog.show(
            () => { },
            () => { },
            new Recipe(),
            ManageMode.Edit);
    }
}
