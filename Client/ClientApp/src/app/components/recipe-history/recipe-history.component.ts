import {Component, OnInit, ViewChild} from '@angular/core';
import {Recipe, RecipeRevision} from '../../models';
import {RecipesService} from '../../services';
import {ActivatedRoute} from '@angular/router';
import {Location} from '@angular/common';
import {ManageMode, ManageRecipeComponent} from '../manage-recipe/manage-recipe.component';

@Component({
    selector: 'app-recipe-history',
    templateUrl: './recipe-history.component.html',
    styleUrls: ['./recipe-history.component.scss']
})
export class RecipeHistoryComponent implements OnInit {
    readonly maxFieldLength = 60;
    public recipeRevisions: RecipeRevision[];
    public recipe: Recipe;
    @ViewChild(ManageRecipeComponent) manageRecipeDialog: ManageRecipeComponent;

    constructor(private recipesService: RecipesService,
                private route: ActivatedRoute,
                private location: Location) {
    }

    ngOnInit(): void {
        let recipeId = +this.route.snapshot.paramMap.get('id');
        this.recipesService.getRecipe(recipeId)
            .subscribe(data => {
                this.recipe = data;
            });

        this.recipesService.getRecipeRevisions(recipeId)
            .subscribe(data => {
                this.recipeRevisions = data
                    .sort((a, b) =>
                        b.startDate.getTime() - a.startDate.getTime()
                    );
            });
    }

    viewRecipe(recipeRevision: RecipeRevision) {
        let recipeClone = JSON.parse(JSON.stringify(this.recipe));
        recipeClone.data = JSON.parse(JSON.stringify(recipeRevision.data));
        this.manageRecipeDialog.show(
            () => {
            },
            () => {
            },
            recipeClone,
            ManageMode.View);
    }

    goBack() {
        this.location.back();
    }

    getShortName(s: string) {
        return s && s.length > this.maxFieldLength ? s.substring(0, this.maxFieldLength) + '...' : s;
    }
}
