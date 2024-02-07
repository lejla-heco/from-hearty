import { Injectable } from "@angular/core";
import { Video } from "./models/video.model";


@Injectable({
    providedIn: 'root'
})
export class VideoService {
    public videos: Video[] = [];

    api = {
        getAllByUserId: '/videos/user',
        createNewVideo: '/videos',
        delete: '/videos'
    }
}