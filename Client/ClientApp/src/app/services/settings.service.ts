import { environment } from "../../environments/environment";
import { Injectable } from "@angular/core";
import { ISettingsService } from "./settings.service.interface";

@Injectable()
export class SettingsService implements ISettingsService {
    constructor() {

    }

    get apiUrl(): string {
        return environment.apiUrl;
    }

    get production(): boolean {
        return environment.production;
    }
}
