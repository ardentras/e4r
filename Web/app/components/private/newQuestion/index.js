import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import Style from "./style.css";
import error from "../../../redux/errorCodes";
import { getQuestions, setAnswer, setIndex, setHelpText, hideHelp } from "../../../redux/actions/questions";
import { setShowQuestion } from "../../../redux/actions/state";
import { handleUserObjectUpdate, solvedQuestion } from "../../../redux/actions/user";
import FooterStyle from "../footer/style.css";

class Question extends React.Component {
	constructor(props) {
		super(props);
		this.getQuestions = this.getQuestions.bind(this);
		this.checkAnswer = this.checkAnswer.bind(this);
		this.nextQuestion = this.nextQuestion.bind(this);
		this.nextBlock = this.nextBlock.bind(this);
	}
	componentWillUnmount() {
		this.props.setShowQuestion(false);
	}
	componentWillMount() {
		this.props.setShowQuestion(true);
		if (!this.props.questions.questions || this.props.questions.questions.length <= 0) {
			this.getQuestions();
		}
	}
	getQuestions() {
		this.props.getQuestions(this.props.user.token, this.props.user.userobject);
	}
	nextBlock() {
		this.getQuestions();
	}
	nextQuestion() {
		const radios = document.getElementsByClassName(Style.radios);
		for (let i = 0; i < radios.length; ++i) {
			radios[i].disabled = false;
			radios[i].checked = false;
		}
		const helper = document.getElementById(FooterStyle.helptext);
		const helpbtn = document.getElementById(FooterStyle.helpbtn);
		if (helper.style.display === "flex") {
			helpbtn.style.background = "white";
			helpbtn.style.color = "#333F4F";
			helper.style.display = "none";
			helper.style.opacity = 0;
			this.props.hideHelp();
		}
		if (this.props.questions.index < 9) {
			this.props.setIndex(this.props.questions.index + 1);
		}
		else {
			this.props.setIndex(0);
			this.getQuestions();
		}
		this.props.hideHelp();
		this.props.setAnswer(undefined);
		this.props.setHelpText(undefined);
	}
	checkAnswer(event) {
		if (event.target.value === this.props.questions.questions[this.props.questions.index].CorrectAnswer) {
			this.props.setAnswer("correct");
			const radios = document.getElementsByClassName(Style.radios);
			for (let i = 0; i < radios.length; ++i) {
				radios[i].disabled = true;
			}
			this.props.solvedQuestion();
			this.props.handleUserObjectUpdate(this.props.user.token,Object.assign({}, this.props.user.userobject, {
				game_data: {
					...this.props.user.userobject.game_data,
					totalQuestions: (parseInt(this.props.user.userobject.game_data.totalQuestions) + 1)
				}
			}));
		}
		else {
			this.props.setAnswer("incorrect");
		}
	}
	render() {
		if (this.props.questions.fetching) {
			return (
				<div className={Style.question}>
					<span>FETCHING QUESTIONS...</span>
				</div>	
			);
		}
		else if (!this.props.questions.questions || this.props.questions.questions.length <= 0 || this.props.ERROR === error.TIME_OUT || this.props.ERROR === error.CONN_FAIL) {
			return (
				<div className={Style.question}>
					<div className={Style.noquestion}>
						<span>{this.props.ERROR ? this.props.ERROR : "No Questions"}</span>
						<button onClick={this.getQuestions} className={Style.retrybtn}>Retry</button>
					</div>
				</div>
			)
		}
		return (
			<div className={Style.question}>
				<div className={Style.gamecontainer}>
					{this.props.questions.answer === "correct" && (<div className={Style.correct}>
						<i id={Style.check} className="fa fa-check-circle"></i>
						<span>Correct</span>
					</div>)}
					{this.props.questions.answer === "incorrect" && (<div className={Style.incorrect}>
						<i id={Style.cross} className="fa fa-times-circle"></i>
						<span>Incorrect</span>
					</div>)}
					<span>{"Question " + (this.props.questions.index + 1) +":"}</span>
					<div className={Style.questioninfo}>
						<h1 className={Style.questiontext}>{this.props.questions.questions[this.props.questions.index].QuestionText}</h1>
						<input onClick={this.checkAnswer} id={Style.answer1} type="radio" name="radios" className={Style.radios} value={this.props.questions.questions[this.props.questions.index].QuestionOne}/>
						<label htmlFor={Style.answer1} className={Style.answer}>
							<span>{this.props.questions.questions[this.props.questions.index].QuestionOne}</span>
						</label>
						<input onClick={this.checkAnswer} id={Style.answer2} type="radio" name="radios" className={Style.radios} value={this.props.questions.questions[this.props.questions.index].QuestionTwo}/>
						<label htmlFor={Style.answer2} className={Style.answer}>
							<span>{this.props.questions.questions[this.props.questions.index].QuestionTwo}</span>
						</label>
						<input onClick={this.checkAnswer} id={Style.answer3} type="radio" name="radios" className={Style.radios} value={this.props.questions.questions[this.props.questions.index].QuestionThree}/>
						<label htmlFor={Style.answer3} className={Style.answer}>
							<span>{this.props.questions.questions[this.props.questions.index].QuestionThree}</span>
						</label>
						<input onClick={this.checkAnswer} id={Style.answer4} type="radio" name="radios" className={Style.radios} value={this.props.questions.questions[this.props.questions.index].QuestionFour}/>
						<label htmlFor={Style.answer4} className={Style.answer}>
							<span>{this.props.questions.questions[this.props.questions.index].QuestionFour}</span>
						</label>
						{this.props.questions.answer === "correct" && ( <button id={Style.nextbtn} onClick={this.nextQuestion}>
							NEXT
						</button>
						)}
					</div>
				</div>
			</div>
		)
	}
}

export default connect(
	(state) => ({user: state.user, questions: state.questions, ERROR: state.state.error}),
	(dispatch) => bindActionCreators({ getQuestions, setShowQuestion, setAnswer, setIndex, setHelpText, hideHelp, handleUserObjectUpdate, solvedQuestion }, dispatch)
)(Question);
