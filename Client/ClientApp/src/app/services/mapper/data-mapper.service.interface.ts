import { Observable } from "rxjs/Observable";
import { RecipeRevision, Recipe, RecipeData } from "../../models";
import { IDataMapper } from "./data-mapper.interface";

export interface IDataMapperService {
    recipeMapper: IDataMapper<Recipe>;

    recipeRevisionMapper: IDataMapper<RecipeRevision>;
}
