using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandID : MonoBehaviour
{
    public const byte COMMAND_PACKET_ID=    0xCA; 
    public const byte MOVE=					0x01; // 0000000   1     4      
	public const byte CAMERA_MOVE=			0x02; // 0000001   0     4 (unsigned short) pan degrees (unsigned short) tilt degrees
	public const byte CAMERA_HOME=			0x04; // 0000010   0     0
	public const byte CAMERA_RESET=			0x05; // 0000010   1     0
	public const byte READ_EEPROM=			0x06; // 0000011   0     3
	public const byte WRITE_EEPROM=			0x07; // 0000011   1     3 + b
	public const byte SAFE_ROVER=			0x08; // 0000100   0     0
	public const byte SLEEP=				0x0A; // 0000101   0     0
	public const byte WAKEUP=				0x0B; // 0000101   1     0
	public const byte HEADLIGHT_OFF=		0x0C; // 0000110   0     0
	public const byte HEADLIGHT_ON=			0x0D; // 0000110   1     0
	public const byte COMM_SETUP=			0x10; // 0001000   0     1 (unsigned byte) mode ID (see McuWatchdogModes)
	public const byte PING=					0x11; // 0001000   1     0
	public const byte HEADING=				0x12; // 0001001   0     2 (unsigned short) degrees
	public const byte CURRENT_COORD=		0x13; // 0001001   1     8 (float) lat (float) lon
	public const byte WAYPOINT_COORD=		0x14; // 0001010   0     9 (float) lat (float) lon (unsigned byte) admin ID
	public const byte WAYPOINTS_OFF=		0x16; // 0001011   0     0
	public const byte WAYPOINTS_ON=			0x17; // 0001011   1     0
	public const byte WAYPOINTS_CLEAR=		0x18; // 0001100   0     0
	public const byte WAYPOINT_MOVE=		0x19; // 0001100   1     9 (float) lat (float) lon (unsigned byte) admin ID
	public const byte WAYPOINT_DELETE=		0x1A; // 0001101   0     1 (unsigned byte) admin ID
	public const byte CAMERA_VIEW_CLICK=	0x1B; // 0001101   1     4 (unsigned short) x (unsigned short) y
	// public const byte SERVO_MOVE=			0x1C; // 0001110   0     1 (unsigned byte) index (unsigned short) position
	public const byte CURRENT_LIMIT=		0x1D; // 0001110   1     1 (unsigned byte) current limit steps N (1 <= N <= 128)
		
}
