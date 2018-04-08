import { Observable } from "rxjs/Observable";
import { RecipeRevision, Recipe } from "../models";

export interface IRecipesService {
    getRecipes(): Observable<Recipe[]>;

    getRecipeRevisions(recipeId: number): Observable<RecipeRevision[]>;
    
    saveRecipe(recipe: Recipe): Observable<Recipe>;

    deleteRecipe(recipeId: number): Observable<boolean>;
}
