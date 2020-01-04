import { Component, OnInit } from '@angular/core';
import { HangmanService } from '../services/hangman.service';
import { Language } from '../models/language.model';
import { Category } from '../models/category.model';
import { Player } from '../models/player.model';
import { Word } from '../models/word.model';
import { StepType } from '../models/steptype.enum';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {


  private player: Player = new Player;

  private step: StepType = StepType.LanguageStep;

  private firstStep: StepType = StepType.LanguageStep;
  private lastStep: StepType = StepType.NameStep; //last step before the game starts
  private gameStep: StepType = StepType.GameStarted;

  private isLanguagesLoading: boolean;
  private languages: Language[] = [];
  private selectedLanguage: Language;
  private alphabetButtons: HTMLCollectionOf<HTMLButtonElement>;

  private isCategoriesLoading: boolean;
  private categories: Category[] = [];
  private selectedCategory: Category;


  randomWord: Word = new Word;
  hiddenWordArray: string[] = [];

  constructor(private hangmanService: HangmanService) { }

  ngOnInit() {
    this.checkStep();
  }

  goToNextStep() {
    this.step += 1;
    this.checkStep();
  }

  goToPrevStep() {
    this.step -= 1;
    this.checkStep();
  }

  goToHome() {
    this.step = this.firstStep;
    this.checkStep();
  }

  checkStep() {
    if (this.step > this.lastStep) this.step = this.lastStep;
    if (this.step < this.firstStep) this.step = this.firstStep;
    switch (this.step) {
      case 0:
        this.isLanguagesLoading = true;
        this.getLanguages();
        break;
      case 1:
        this.isCategoriesLoading = true;
        this.getCategories();
        break;
      case 2:
        this.initPlayer();
        break;
      case 3:
        this.startGame();
        break;
    }
  }

  getLanguages() {
    this.languages = [];
    this.hangmanService.getLanguages().subscribe(langs => {
      this.languages = langs;
      this.isLanguagesLoading = false;
      this.selectedLanguage = this.languages[0];
      this.alphabetButtons = (document.getElementsByClassName('btn-alphabet') as HTMLCollectionOf<HTMLButtonElement>);
    });
  }

  getCategories() {
    this.categories = [];
    this.hangmanService.getCategories(this.selectedLanguage.id).subscribe(cats => {
      this.categories = cats;
      this.isCategoriesLoading = false;
      this.selectedCategory = this.categories[0];
    });
  }

  initPlayer() {
    this.player.name = this.player.name;
    this.player.score = 0;
    this.player.totalWins = 0;
    this.player.failCount = 0;
    this.player.maxTryCount = 5;
    this.player.remainingLife = 5;
    this.player.hasWon = false;
  }

  pickLanguage(lang: Language) {
    this.selectedLanguage = lang;
  }

  pickCategory(cat: Category) {
    this.selectedCategory = cat;
  }

  async startGame() {
    this.disableAllAlphabetButtons(false);
    this.initPlayer();
    this.step = StepType.GameStarted;
    this.randomWord = await this.hangmanService.initGame(this.player, this.selectedLanguage, this.selectedCategory);
    this.hiddenWordArray = this.hangmanService.getHiddenTextArray;
  }

  async restartGame() {
    this.disableAllAlphabetButtons(false);
    this.randomWord = await this.hangmanService.initGame(this.player, this.selectedLanguage, this.selectedCategory);
    this.hiddenWordArray = this.hangmanService.getHiddenTextArray;
  }

  checkALetter(btn: HTMLButtonElement, letter: string) {
    this.hangmanService.checkLetter(letter);
    btn.disabled = true;
    if (this.hangmanService.getIsGameFinished && this.player.hasWon) {
      this.disableAllAlphabetButtons(true);
    } else if (this.hangmanService.getIsGameFinished && !this.player.hasWon) {
      this.disableAllAlphabetButtons(true);
    }
  }

  disableAllAlphabetButtons(status: boolean) {
    for (let i = 0; i < this.alphabetButtons.length; i++) {
      this.alphabetButtons[i].disabled = status;
    }
  }
}
