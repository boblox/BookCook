import { environment } from "../../environments/environment";
import { Injectable } from "@angular/core";

export interface ISettingsService {
    readonly apiUrl: string;

    readonly production: boolean;
}
