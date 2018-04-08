//import { IApiModel } from "./api-model.interface";

export class RecipeData /*implements IApiModel*/ {
    name: string;
    description: string;

    public constructor(init?: Partial<RecipeData>) {
        Object.assign(this, init);
    }
}
