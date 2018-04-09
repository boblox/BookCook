import {RecipeData} from './recipe-data.model';

export class RecipeRevision  {
    id: number;
    version: number;
    startDate: Date;
    endDate: Date;
    data: RecipeData;

    public constructor(init?: Partial<RecipeRevision>) {
        Object.assign(this, init);
    }
}
