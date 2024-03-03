using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Object = UnityEngine.Object;

    [RequireComponent(typeof(TMP_Text))]
    public class TextScroll : MonoBehaviour
    {

        string[] textsToDisplay = {
            "first! woo you did it wow.",
            "second set of text. did it work?",
            "placeholder"
        };

        private TMP_Text _textBox;
        // Basic Typewriter Functionality
        private int _currentVisibleCharacterIndex;
        private Coroutine _typewriterCoroutine;
        private bool _readyForNewText = true;
        private WaitForSeconds _simpleDelay;
        private WaitForSeconds _interpunctuationDelay;
        [Header("Typewriter Settings")] 
        [SerializeField] private float charactersPerSecond = 20;
        [SerializeField] private float interpunctuationDelay = 0.5f;
        public Animator animator;
        public SpriteRenderer tvAnimSpriteRenderer;

        // Skipping Functionality
        public bool CurrentlySkipping { get; private set; }
        private WaitForSeconds _skipDelay;

        [Header("Skip options")] 
        [SerializeField] private bool quickSkip;
        [SerializeField] [Min(1)] private int skipSpeedup = 5;


        // Event Functionality
        private WaitForSeconds _textboxFullEventDelay;
        [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f; // In testing, I found 0.25 to be a good value

        public static event Action CompleteTextRevealed;
        public static event Action<char> CharacterRevealed;

        private void Awake()
        {
            _textBox = GetComponent<TMP_Text>();
            _textBox.maxVisibleCharacters = 0;
            _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
            _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
            _textBox.text = textsToDisplay[SM.textsToDisplayIterator];
            SM.textsToDisplayIterator = SM.textsToDisplayIterator + 1;
            
            _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
            _textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
        }
        
        private void OnEnable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(PrepareForNewText);
        }

        private void OnDisable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(PrepareForNewText);
        }

        #region Skipfunctionality
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (_textBox.maxVisibleCharacters != _textBox.textInfo.characterCount - 1)
                    Skip();
            }
        }
         #endregion

         private void PrepareForNewText(Object obj)
         {
             if (obj != _textBox || !_readyForNewText || _textBox.maxVisibleCharacters >= _textBox.textInfo.characterCount)
                 return;
             
             CurrentlySkipping = false;
             _readyForNewText = false;
            
             if (_typewriterCoroutine != null)
                 StopCoroutine(_typewriterCoroutine);
            
             _textBox.maxVisibleCharacters = 0;
             _currentVisibleCharacterIndex = 0;

             _typewriterCoroutine = StartCoroutine(Typewriter());
         }

        private IEnumerator Typewriter()
        {
            TMP_TextInfo textInfo = _textBox.textInfo;
            while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
            {
                var lastCharacterIndex = textInfo.characterCount - 1;

                if (_currentVisibleCharacterIndex >= lastCharacterIndex)
                {
                    _textBox.maxVisibleCharacters++;
                    yield return _textboxFullEventDelay;
                    CompleteTextRevealed?.Invoke();
                    _readyForNewText = true;
                    animator.speed = 0;
                    yield return _textboxFullEventDelay;
                    _textBox.text = textsToDisplay[SM.textsToDisplayIterator];
                    // TODO: put a blocker in front of the TV when the intro anim is done.
                    // TODO: then reach into the camera script and zoom out, and begin the rest of the stuff.
                    Debug.Log(SM.textsToDisplayIterator);
                    if (SM.textsToDisplayIterator == 2) { // done with intro text, pause intro animation
                        SM.beginGamePostIntro = true;
                    }
                    animator.speed = 1;
                    if (SM.beginGamePostIntro) {
                        animator.speed = 0;
                        // enable TV blocker
                        tvAnimSpriteRenderer.enabled = false;

                    }
                    SM.textsToDisplayIterator = SM.textsToDisplayIterator + 1;
                    yield break;
                }

                char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

                _textBox.maxVisibleCharacters++;
                
                if (!CurrentlySkipping &&
                    (character == '?' || character == '.' || character == ',' || character == ':' ||
                     character == ';' || character == '!' || character == '-'))
                {
                    yield return _interpunctuationDelay;
                }
                else
                {
                    yield return CurrentlySkipping ? _skipDelay : _simpleDelay;
                }
                
                CharacterRevealed?.Invoke(character);
                _currentVisibleCharacterIndex++;
            }
        }

        private void Skip(bool quickSkipNeeded = false)
        {
            if (CurrentlySkipping)
                return;
            
            CurrentlySkipping = true;

            if (!quickSkip || !quickSkipNeeded)
            {
                StartCoroutine(SkipSpeedupReset());
                return;
            }

            StopCoroutine(_typewriterCoroutine);
            _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
            _readyForNewText = true;
            CompleteTextRevealed?.Invoke();
        }

        private IEnumerator SkipSpeedupReset()
        {
            yield return new WaitUntil(() => _textBox.maxVisibleCharacters == _textBox.textInfo.characterCount - 1);
            CurrentlySkipping = false;
        }
    }