import { RecipeData } from "./recipe-data.model";
import { RecipeRevision } from "./recipe-revision.model";

export class Recipe{
    id: number;
    dateCreated: Date;
    data: RecipeData;
    historicalRevisions: RecipeRevision[] = [];

    public constructor(init?: Partial<Recipe>) {
        Object.assign(this, init);
    }
}
