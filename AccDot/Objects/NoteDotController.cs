using System.Collections;
using UnityEngine;
using Zenject;
using static IPA.Utilities.ReflectionUtil;

namespace AccDot.Objects
{
    public class NoteDotController : MonoBehaviour
    {
        [Inject]
        private readonly Dot _dot;

        [Inject]
        private readonly Config _config;

        private GameNoteController _noteController;
        private NoteJump _jump;

        public void Start()
        {
            _noteController = GetComponent<GameNoteController>();
            _jump = GetComponent<NoteJump>();
            _noteController.cubeNoteControllerDidInitEvent += Poggers;
        }

        public void Poggers(GameNoteController _)
        {
            //caeden attempted to wait 1 second, L + Ratio + Cancel him on twitter + He fell off + ChroWhatter + Cringers+ + EnviroWhat
            //                        ^ USING A FUCKING IENUMERATOR AND YIELD RETURN :skull:
            // yall cant handle my fucking massive brain ~caeden - crypto L 

            _dot.gameObject.SetActive(true);

            var _startPos = _jump.GetField<Vector3, NoteJump>("_startPos");
            var _startVerticalVelocity = _jump.GetField<float, NoteJump>("_startVerticalVelocity");
            var _gravity = _jump.GetField<float, NoteJump>("_gravity");
            var _beatTime = _jump.GetField<float, NoteJump>("_beatTime"); var _jumpDuration = _jump.GetField<float, NoteJump>("_jumpDuration");

            var num = _jumpDuration * 0.5f;
            var pos = (_startPos - _noteController.noteMovement.beatPos).normalized * _config.Distance + _noteController.noteMovement.beatPos;

            _dot.transform.position = new Vector3(pos.x,
                (_startPos.y + _startVerticalVelocity * num - _gravity * num * num * 0.5f),
                pos.z);

            _dot.transform.rotation = _noteController.transform.rotation;
        }//pop pop pop pop pop pop pop pop

        public void OnDisable()
        {
            byebye();
        }

        public void Update()
        {
            var _atsc = _jump.GetField<IAudioTimeSource, NoteJump>("_audioTimeSyncController");
            var _beatTime = _jump.GetField<float, NoteJump>("_beatTime");
            var _jumpDuration = _jump.GetField<float, NoteJump>("_jumpDuration");

            if (_atsc.songTime - (_beatTime - _jumpDuration * 0.5f) >= _jumpDuration * 0.5f)
            {
                byebye();
            }

            //_cutout.SetCutout(Mathf.Abs(_noteController.noteMovement.distanceToPlayer - _noteController.jumpStartPos.z));



            /*var _startPos = _jump.GetField<Vector3, NoteJump>("_startPos");
            var _startVerticalVelocity = _jump.GetField<float, NoteJump>("_startVerticalVelocity");
            var _gravity = _jump.GetField<float, NoteJump>("_gravity");
            var _beatTime = _jump.GetField<float, NoteJump>("_beatTime");
            var _jumpDuration = _jump.GetField<float, NoteJump>("_jumpDuration");
            var _atsc = _jump.GetField<IAudioTimeSource, NoteJump>("_audioTimeSyncController");

            float songTime = _atsc.songTime;
            float num = songTime - (_beatTime - _jumpDuration * 0.5f);
            float num2 = num / _jumpDuration;

            _dot.transform.position = new Vector3(_noteController.noteMovement.beatPos.x,
                _startPos.y + _startVerticalVelocity * num - _gravity * num * num * 0.5f + 0.25f,
                _noteController.noteMovement.beatPos.z);*/
        }

        public void byebye()
        {
            _dot.gameObject.SetActive(false);
        }
    }
}