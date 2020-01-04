import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Language } from '../models/language.model';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model';
import { Player } from '../models/player.model';
import { Word } from '../models/word.model';

@Injectable({
    providedIn: 'root'
})

export class HangmanService {
    private player: Player;
    private language: Language;
    private category: Category;

    private randomWord: Word;

    private wordArray: string[] = [];
    private hiddenWordArray: string[] = [];
    private returnIndexArray: number[] = [];

    private isGameFinished: boolean;

    private url: string = '/api/'
    constructor(
        private http: HttpClient
    ) { }

    getLanguages(): Observable<Language[]> {
        return this.http.get<Language[]>(this.url + 'home/GetLanguages');
    }

    getCategories(languageId: number): Observable<Category[]> {
        return this.http.get<Category[]>(this.url + 'home/GetCategories?id=' + languageId);
    }

    private async getRandomWord(categoryId: number): Promise<Word> {
        return await this.http.get<Word>(this.url + 'game/GetRandomWord?categoryId=' + categoryId).toPromise();
    }

    async initGame(p: Player, lang: Language, cat: Category): Promise<Word> {
        this.randomWord = new Word;
        this.hiddenWordArray = [];
        this.player = p;
        this.language = lang;
        this.category = cat;
        this.isGameFinished = false;
        await this.getRandomWord(this.category.id).then(word => {
            this.randomWord = word;
            this.wordArray = this.randomWord.word.split('');
            this.wordArray.forEach((char) => {
                this.hiddenWordArray.push(' ');
            });
        });
        return this.randomWord;
    }

    get getAlphabet(): string[] {
        return this.language.alphabet.split('');
    }

    get getHiddenTextArray(): string[] {
        return this.hiddenWordArray;
    }

    get getIsGameFinished(): boolean {
        return this.isGameFinished;
    }

    checkLetter(char: string) {
        if (!this.isGameFinished) {
            this.returnIndexArray = [];
            let i: number = 0;
            const gameTextCharArray: string[] = this.randomWord.word.split('');
            gameTextCharArray.forEach(c => {
                if (char == c) {
                    this.returnIndexArray.push(i);
                }
                i++;
            });

            if (this.returnIndexArray.length > 0) {
                this.player.score += this.returnIndexArray.length;
                this.returnIndexArray.forEach(index => {
                    this.hiddenWordArray[index] = this.wordArray[index];
                });
            } else {
                this.player.failCount += 1;
                this.player.remainingLife -= 1;
            }
        }
        if (this.checkIfGameFinished()) {
            this.isGameFinished = true;
        } else {
            this.isGameFinished = false;
        }
    }

    private checkIfGameFinished() {
        if (this.player.maxTryCount == this.player.failCount || this.player.remainingLife == 0) {
            this.player.hasWon = false;
            return true;
        } else if (JSON.stringify(this.hiddenWordArray) === JSON.stringify(this.wordArray)) {
            this.player.totalWins += 1;
            this.player.hasWon = true;
            return true;
        }
        else {
            return false;
        }
    }
}