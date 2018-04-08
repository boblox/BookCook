//import { IApiModel } from "./api-model.interface";
import { RecipeData } from "./recipe-data.model";

export class RecipeRevision /*implements IApiModel*/ {
    id: number;
    version: number;
    startDate: Date;
    endDate: Date;
    data: RecipeData;
}
