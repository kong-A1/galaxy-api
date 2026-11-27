using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace galaxy_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "users",
                comment: "ผู้ใช้ที่ล็อกอินเข้าระบบ");

            migrationBuilder.AlterTable(
                name: "user_roles",
                comment: "บทบาทที่กำหนดให้ผู้ใช้");

            migrationBuilder.AlterTable(
                name: "roles",
                comment: "บทบาทของผู้ใช้");

            migrationBuilder.AlterTable(
                name: "refresh_tokens",
                comment: "refresh token สำหรับต่ออายุการล็อกอิน (เก็บเฉพาะ hash)");

            migrationBuilder.AlterTable(
                name: "permissions",
                comment: "รายการสิทธิ์ในระบบ");

            migrationBuilder.AlterTable(
                name: "permission_role",
                comment: "สิทธิ์ที่มอบให้บทบาท");

            migrationBuilder.AlterTable(
                name: "password_reset_tokens",
                comment: "คำขอรีเซ็ตรหัสผ่าน (เก็บเฉพาะ hash ของโทเคน)");

            migrationBuilder.AlterTable(
                name: "menu_permissions",
                comment: "สิทธิ์ที่ใช้ควบคุมการเห็นเมนู");

            migrationBuilder.AlterTable(
                name: "master_menus",
                comment: "เมนูของระบบ");

            migrationBuilder.AlterTable(
                name: "audit_logs",
                comment: "ประวัติการกระทำในระบบ");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_by",
                table: "users",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่แก้ไขแถวนี้ล่าสุด",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "updated_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่แก้ไขล่าสุด (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                table: "users",
                type: "text",
                nullable: false,
                comment: "รหัสผ่านที่ผ่านการ hash แล้ว (เช่น Argon2id/bcrypt) ห้ามเก็บรหัสผ่านจริง",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "password_changed_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                comment: "เวลาที่เปลี่ยนรหัสผ่านล่าสุด (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                comment: "ชื่อ-นามสกุลของพนักงานที่แสดงในระบบ",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "must_change_password",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "บังคับเปลี่ยนรหัสผ่านเมื่อล็อกอินครั้งถัดไป",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "locked_until",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                comment: "เวลาที่บัญชีถูกล็อกจนถึง ว่าง = ไม่ถูกล็อก",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "last_login_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                comment: "เวลาที่ล็อกอินสำเร็จล่าสุด (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                comment: "เปิด/ปิดบัญชีชั่วคราว FALSE = ล็อกอินไม่ได้ (ต่างจากการลบ ซึ่งใช้ deleted_at)",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<int>(
                name: "failed_login_count",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "จำนวนครั้งที่ใส่รหัสผ่านผิดติดต่อกัน ใช้ล็อกบัญชีเมื่อเกินกำหนด",
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                comment: "อีเมลที่ใช้ล็อกอิน ไม่ซ้ำ (นับเฉพาะผู้ใช้ที่ยังไม่ถูกลบ ไม่แยกตัวพิมพ์เล็ก/ใหญ่)",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_by",
                table: "users",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่ลบแถวนี้ (soft delete)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                comment: "เวลาที่ลบแบบ soft delete ว่าง = ยังใช้งานอยู่",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "users",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่สร้างแถวนี้ (ว่างได้สำหรับข้อมูลที่ระบบสร้าง)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่สร้างแถว (เก็บเป็น UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "รหัสของแถว (UUID)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "user_roles",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่เป็นคนกำหนดบทบาทนี้ให้",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "user_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่กำหนดบทบาท (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<Guid>(
                name: "role_id",
                table: "user_roles",
                type: "uuid",
                nullable: false,
                comment: "บทบาทที่ถูกกำหนดให้ผู้ใช้ (FK roles.id)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "user_roles",
                type: "uuid",
                nullable: false,
                comment: "ผู้ใช้ที่ได้รับบทบาท (FK users.id)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_by",
                table: "roles",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่แก้ไขแถวนี้ล่าสุด",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "updated_at",
                table: "roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่แก้ไขล่าสุด (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "roles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                comment: "ชื่อบทบาทที่แสดงบนหน้าจอ",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "is_system",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "บทบาทที่ระบบสร้างให้ (seed) ห้ามแก้ไขหรือลบผ่านหน้าจอ",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "is_super_admin",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "TRUE = ผู้ดูแลสูงสุด ผ่านการตรวจสิทธิ์ทุกอย่าง",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                comment: "เปิด/ปิดบทบาทชั่วคราว FALSE = ผู้ใช้ที่มีบทบาทนี้ไม่ได้รับสิทธิ์จากบทบาทนี้",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_by",
                table: "roles",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่ลบแถวนี้ (soft delete)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at",
                table: "roles",
                type: "timestamp with time zone",
                nullable: true,
                comment: "เวลาที่ลบแบบ soft delete ว่าง = ยังใช้งานอยู่",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "roles",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่สร้างแถวนี้ (ว่างได้สำหรับข้อมูลที่ระบบสร้าง)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่สร้างแถว (เก็บเป็น UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "roles",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                comment: "รหัสบทบาทสำหรับอ้างอิงในโค้ด ไม่ซ้ำ (นับเฉพาะแถวที่ยังไม่ถูกลบ)",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "roles",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "รหัสของแถว (UUID)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "refresh_tokens",
                type: "uuid",
                nullable: false,
                comment: "ผู้ใช้เจ้าของโทเคน (FK users.id)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "user_agent",
                table: "refresh_tokens",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                comment: "User-Agent ของอุปกรณ์/เบราว์เซอร์ที่ขอโทเคน",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "token_hash",
                table: "refresh_tokens",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                comment: "ค่า hash ของ refresh token เก็บเฉพาะ hash ห้ามเก็บโทเคนจริง",
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "revoked_at",
                table: "refresh_tokens",
                type: "timestamp with time zone",
                nullable: true,
                comment: "เวลาที่โทเคนถูกเพิกถอน ว่าง = ยังไม่ถูกเพิกถอน",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "family_id",
                table: "refresh_tokens",
                type: "uuid",
                nullable: false,
                comment: "รหัสกลุ่มโทเคนที่หมุนเวียนต่อกันจากการล็อกอินครั้งเดียว ใช้ตรวจจับการใช้โทเคนซ้ำ",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "expires_at",
                table: "refresh_tokens",
                type: "timestamp with time zone",
                nullable: false,
                comment: "เวลาที่โทเคนหมดอายุ (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "created_ip",
                table: "refresh_tokens",
                type: "character varying(45)",
                maxLength: 45,
                nullable: true,
                comment: "IP ของผู้ที่ขอโทเคน",
                oldClrType: typeof(string),
                oldType: "character varying(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "refresh_tokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่ออกโทเคน (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "refresh_tokens",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "รหัสโทเคน (UUID)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "updated_at",
                table: "permissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่แก้ไขล่าสุด (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "permissions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                comment: "ชื่อสิทธิ์ที่แสดงบนหน้าจอจัดการ role",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "module",
                table: "permissions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                comment: "กลุ่มของสิทธิ์ ใช้จัดหมวดบนหน้าจอจัดการ role",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "permissions",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                comment: "ปิดสิทธิ์ชั่วคราวโดยไม่ต้องลบ FALSE = ไม่มีผลกับทุก role",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "permissions",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                comment: "คำอธิบายว่าสิทธิ์นี้ทำอะไรได้บ้าง",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "permissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่สิทธิ์นี้ถูกซิงก์เข้าฐานข้อมูลครั้งแรก (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "permissions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                comment: "รหัสสิทธิ์รูปแบบ resource:action เช่น member:read ใช้ในโค้ดตรวจสิทธิ์ ไม่ซ้ำ",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "permissions",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "รหัสสิทธิ์ (UUID)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<Guid>(
                name: "permission_id",
                table: "permission_role",
                type: "uuid",
                nullable: false,
                comment: "สิทธิ์ที่ถูกมอบให้บทบาท (FK permissions.id)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "role_id",
                table: "permission_role",
                type: "uuid",
                nullable: false,
                comment: "บทบาทที่ได้รับสิทธิ์ (FK roles.id)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "password_reset_tokens",
                type: "uuid",
                nullable: false,
                comment: "ผู้ใช้ที่ขอรีเซ็ตรหัสผ่าน (FK users.id)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "used_at",
                table: "password_reset_tokens",
                type: "timestamp with time zone",
                nullable: true,
                comment: "เวลาที่ใช้โทเคนนี้แล้ว ว่าง = ยังไม่ถูกใช้",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "token_hash",
                table: "password_reset_tokens",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                comment: "ค่า hash ของโทเคนที่ส่งทางอีเมล ห้ามเก็บโทเคนจริง",
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "expires_at",
                table: "password_reset_tokens",
                type: "timestamp with time zone",
                nullable: false,
                comment: "เวลาที่ลิงก์รีเซ็ตหมดอายุ (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "password_reset_tokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่สร้างคำขอ (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "password_reset_tokens",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "รหัสคำขอรีเซ็ตรหัสผ่าน (UUID)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<Guid>(
                name: "permission_id",
                table: "menu_permissions",
                type: "uuid",
                nullable: false,
                comment: "สิทธิ์ที่ต้องมีเพื่อเห็นเมนูนี้ (FK permissions.id)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "menu_id",
                table: "menu_permissions",
                type: "uuid",
                nullable: false,
                comment: "เมนูที่ถูกควบคุมสิทธิ์ (FK master_menus.id)",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_by",
                table: "master_menus",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่แก้ไขแถวนี้ล่าสุด",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "updated_at",
                table: "master_menus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่แก้ไขล่าสุด (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<int>(
                name: "sort_order",
                table: "master_menus",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "ลำดับการแสดงภายใต้เมนูแม่เดียวกัน (น้อยก่อน)",
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "route",
                table: "master_menus",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                comment: "path ของหน้าจอที่เมนูนี้เปิด ว่างได้เมื่อเป็นเมนูกลุ่มที่ไม่มีหน้าของตัวเอง",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "parent_id",
                table: "master_menus",
                type: "uuid",
                nullable: true,
                comment: "เมนูแม่ (FK master_menus.id) NULL = เมนูระดับบนสุด",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name_th",
                table: "master_menus",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                comment: "ชื่อเมนูภาษาไทย",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "name_en",
                table: "master_menus",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                comment: "ชื่อเมนูภาษาอังกฤษ",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "master_menus",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                comment: "ซ่อน/แสดงเมนูโดยไม่ต้องลบ",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "icon",
                table: "master_menus",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "ชื่อไอคอนที่แสดงข้างเมนู",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_by",
                table: "master_menus",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่ลบแถวนี้ (soft delete)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at",
                table: "master_menus",
                type: "timestamp with time zone",
                nullable: true,
                comment: "เวลาที่ลบแบบ soft delete ว่าง = ยังใช้งานอยู่",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "master_menus",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่สร้างแถวนี้ (ว่างได้สำหรับข้อมูลที่ระบบสร้าง)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "master_menus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่สร้างแถว (เก็บเป็น UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "master_menus",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                comment: "รหัสเมนูสำหรับอ้างอิงข้ามระบบ ไม่ซ้ำ ไม่เป็นค่าว่าง",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "master_menus",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "รหัสของแถว (UUID)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AlterColumn<string>(
                name: "user_agent",
                table: "audit_logs",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                comment: "User-Agent ของผู้กระทำ",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "platform_actor_id",
                table: "audit_logs",
                type: "uuid",
                nullable: true,
                comment: "คอลัมน์จากแบบเดิมที่มีฐานกลาง ไม่มี FK (รอทบทวน)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "old_values",
                table: "audit_logs",
                type: "jsonb",
                nullable: true,
                comment: "ค่าก่อนเปลี่ยน",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "new_values",
                table: "audit_logs",
                type: "jsonb",
                nullable: true,
                comment: "ค่าหลังเปลี่ยน",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ip_address",
                table: "audit_logs",
                type: "character varying(45)",
                maxLength: 45,
                nullable: true,
                comment: "IP ของผู้กระทำ",
                oldClrType: typeof(string),
                oldType: "character varying(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "entity_type",
                table: "audit_logs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                comment: "ประเภทข้อมูลที่ถูกกระทำ เช่น role, user, menu",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "entity_id",
                table: "audit_logs",
                type: "uuid",
                nullable: true,
                comment: "รหัสของข้อมูลที่ถูกกระทำ",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "audit_logs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                comment: "เวลาที่เกิดเหตุการณ์ (UTC)",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<Guid>(
                name: "actor_user_id",
                table: "audit_logs",
                type: "uuid",
                nullable: true,
                comment: "ผู้ใช้ที่ทำรายการ (FK users.id)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "actor_type",
                table: "audit_logs",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                comment: "ประเภทผู้กระทำ: user = ผู้ใช้ในระบบ, system = ระบบ, platform = ค่าจากแบบเดิม (รอทบทวน)",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "action",
                table: "audit_logs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                comment: "ชื่อการกระทำ",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "audit_logs",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                comment: "รหัสบันทึก audit (UUID)",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "users",
                oldComment: "ผู้ใช้ที่ล็อกอินเข้าระบบ");

            migrationBuilder.AlterTable(
                name: "user_roles",
                oldComment: "บทบาทที่กำหนดให้ผู้ใช้");

            migrationBuilder.AlterTable(
                name: "roles",
                oldComment: "บทบาทของผู้ใช้");

            migrationBuilder.AlterTable(
                name: "refresh_tokens",
                oldComment: "refresh token สำหรับต่ออายุการล็อกอิน (เก็บเฉพาะ hash)");

            migrationBuilder.AlterTable(
                name: "permissions",
                oldComment: "รายการสิทธิ์ในระบบ");

            migrationBuilder.AlterTable(
                name: "permission_role",
                oldComment: "สิทธิ์ที่มอบให้บทบาท");

            migrationBuilder.AlterTable(
                name: "password_reset_tokens",
                oldComment: "คำขอรีเซ็ตรหัสผ่าน (เก็บเฉพาะ hash ของโทเคน)");

            migrationBuilder.AlterTable(
                name: "menu_permissions",
                oldComment: "สิทธิ์ที่ใช้ควบคุมการเห็นเมนู");

            migrationBuilder.AlterTable(
                name: "master_menus",
                oldComment: "เมนูของระบบ");

            migrationBuilder.AlterTable(
                name: "audit_logs",
                oldComment: "ประวัติการกระทำในระบบ");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_by",
                table: "users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่แก้ไขแถวนี้ล่าสุด");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "updated_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่แก้ไขล่าสุด (UTC)");

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                table: "users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "รหัสผ่านที่ผ่านการ hash แล้ว (เช่น Argon2id/bcrypt) ห้ามเก็บรหัสผ่านจริง");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "password_changed_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "เวลาที่เปลี่ยนรหัสผ่านล่าสุด (UTC)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldComment: "ชื่อ-นามสกุลของพนักงานที่แสดงในระบบ");

            migrationBuilder.AlterColumn<bool>(
                name: "must_change_password",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false,
                oldComment: "บังคับเปลี่ยนรหัสผ่านเมื่อล็อกอินครั้งถัดไป");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "locked_until",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "เวลาที่บัญชีถูกล็อกจนถึง ว่าง = ไม่ถูกล็อก");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "last_login_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "เวลาที่ล็อกอินสำเร็จล่าสุด (UTC)");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true,
                oldComment: "เปิด/ปิดบัญชีชั่วคราว FALSE = ล็อกอินไม่ได้ (ต่างจากการลบ ซึ่งใช้ deleted_at)");

            migrationBuilder.AlterColumn<int>(
                name: "failed_login_count",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0,
                oldComment: "จำนวนครั้งที่ใส่รหัสผ่านผิดติดต่อกัน ใช้ล็อกบัญชีเมื่อเกินกำหนด");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldComment: "อีเมลที่ใช้ล็อกอิน ไม่ซ้ำ (นับเฉพาะผู้ใช้ที่ยังไม่ถูกลบ ไม่แยกตัวพิมพ์เล็ก/ใหญ่)");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_by",
                table: "users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่ลบแถวนี้ (soft delete)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "เวลาที่ลบแบบ soft delete ว่าง = ยังใช้งานอยู่");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่สร้างแถวนี้ (ว่างได้สำหรับข้อมูลที่ระบบสร้าง)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่สร้างแถว (เก็บเป็น UTC)");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "รหัสของแถว (UUID)");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "user_roles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่เป็นคนกำหนดบทบาทนี้ให้");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "user_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่กำหนดบทบาท (UTC)");

            migrationBuilder.AlterColumn<Guid>(
                name: "role_id",
                table: "user_roles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "บทบาทที่ถูกกำหนดให้ผู้ใช้ (FK roles.id)");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "user_roles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "ผู้ใช้ที่ได้รับบทบาท (FK users.id)");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_by",
                table: "roles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่แก้ไขแถวนี้ล่าสุด");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "updated_at",
                table: "roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่แก้ไขล่าสุด (UTC)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "roles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldComment: "ชื่อบทบาทที่แสดงบนหน้าจอ");

            migrationBuilder.AlterColumn<bool>(
                name: "is_system",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false,
                oldComment: "บทบาทที่ระบบสร้างให้ (seed) ห้ามแก้ไขหรือลบผ่านหน้าจอ");

            migrationBuilder.AlterColumn<bool>(
                name: "is_super_admin",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false,
                oldComment: "TRUE = ผู้ดูแลสูงสุด ผ่านการตรวจสิทธิ์ทุกอย่าง");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "roles",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true,
                oldComment: "เปิด/ปิดบทบาทชั่วคราว FALSE = ผู้ใช้ที่มีบทบาทนี้ไม่ได้รับสิทธิ์จากบทบาทนี้");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_by",
                table: "roles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่ลบแถวนี้ (soft delete)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at",
                table: "roles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "เวลาที่ลบแบบ soft delete ว่าง = ยังใช้งานอยู่");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "roles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่สร้างแถวนี้ (ว่างได้สำหรับข้อมูลที่ระบบสร้าง)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่สร้างแถว (เก็บเป็น UTC)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "roles",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldComment: "รหัสบทบาทสำหรับอ้างอิงในโค้ด ไม่ซ้ำ (นับเฉพาะแถวที่ยังไม่ถูกลบ)");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "roles",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "รหัสของแถว (UUID)");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "refresh_tokens",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "ผู้ใช้เจ้าของโทเคน (FK users.id)");

            migrationBuilder.AlterColumn<string>(
                name: "user_agent",
                table: "refresh_tokens",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldComment: "User-Agent ของอุปกรณ์/เบราว์เซอร์ที่ขอโทเคน");

            migrationBuilder.AlterColumn<string>(
                name: "token_hash",
                table: "refresh_tokens",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64,
                oldComment: "ค่า hash ของ refresh token เก็บเฉพาะ hash ห้ามเก็บโทเคนจริง");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "revoked_at",
                table: "refresh_tokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "เวลาที่โทเคนถูกเพิกถอน ว่าง = ยังไม่ถูกเพิกถอน");

            migrationBuilder.AlterColumn<Guid>(
                name: "family_id",
                table: "refresh_tokens",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "รหัสกลุ่มโทเคนที่หมุนเวียนต่อกันจากการล็อกอินครั้งเดียว ใช้ตรวจจับการใช้โทเคนซ้ำ");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "expires_at",
                table: "refresh_tokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldComment: "เวลาที่โทเคนหมดอายุ (UTC)");

            migrationBuilder.AlterColumn<string>(
                name: "created_ip",
                table: "refresh_tokens",
                type: "character varying(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldComment: "IP ของผู้ที่ขอโทเคน");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "refresh_tokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่ออกโทเคน (UTC)");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "refresh_tokens",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "รหัสโทเคน (UUID)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "updated_at",
                table: "permissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่แก้ไขล่าสุด (UTC)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "permissions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldComment: "ชื่อสิทธิ์ที่แสดงบนหน้าจอจัดการ role");

            migrationBuilder.AlterColumn<string>(
                name: "module",
                table: "permissions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldComment: "กลุ่มของสิทธิ์ ใช้จัดหมวดบนหน้าจอจัดการ role");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "permissions",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true,
                oldComment: "ปิดสิทธิ์ชั่วคราวโดยไม่ต้องลบ FALSE = ไม่มีผลกับทุก role");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "permissions",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldComment: "คำอธิบายว่าสิทธิ์นี้ทำอะไรได้บ้าง");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "permissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่สิทธิ์นี้ถูกซิงก์เข้าฐานข้อมูลครั้งแรก (UTC)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "permissions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldComment: "รหัสสิทธิ์รูปแบบ resource:action เช่น member:read ใช้ในโค้ดตรวจสิทธิ์ ไม่ซ้ำ");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "permissions",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "รหัสสิทธิ์ (UUID)");

            migrationBuilder.AlterColumn<Guid>(
                name: "permission_id",
                table: "permission_role",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "สิทธิ์ที่ถูกมอบให้บทบาท (FK permissions.id)");

            migrationBuilder.AlterColumn<Guid>(
                name: "role_id",
                table: "permission_role",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "บทบาทที่ได้รับสิทธิ์ (FK roles.id)");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "password_reset_tokens",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "ผู้ใช้ที่ขอรีเซ็ตรหัสผ่าน (FK users.id)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "used_at",
                table: "password_reset_tokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "เวลาที่ใช้โทเคนนี้แล้ว ว่าง = ยังไม่ถูกใช้");

            migrationBuilder.AlterColumn<string>(
                name: "token_hash",
                table: "password_reset_tokens",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64,
                oldComment: "ค่า hash ของโทเคนที่ส่งทางอีเมล ห้ามเก็บโทเคนจริง");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "expires_at",
                table: "password_reset_tokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldComment: "เวลาที่ลิงก์รีเซ็ตหมดอายุ (UTC)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "password_reset_tokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่สร้างคำขอ (UTC)");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "password_reset_tokens",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "รหัสคำขอรีเซ็ตรหัสผ่าน (UUID)");

            migrationBuilder.AlterColumn<Guid>(
                name: "permission_id",
                table: "menu_permissions",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "สิทธิ์ที่ต้องมีเพื่อเห็นเมนูนี้ (FK permissions.id)");

            migrationBuilder.AlterColumn<Guid>(
                name: "menu_id",
                table: "menu_permissions",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "เมนูที่ถูกควบคุมสิทธิ์ (FK master_menus.id)");

            migrationBuilder.AlterColumn<Guid>(
                name: "updated_by",
                table: "master_menus",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่แก้ไขแถวนี้ล่าสุด");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "updated_at",
                table: "master_menus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่แก้ไขล่าสุด (UTC)");

            migrationBuilder.AlterColumn<int>(
                name: "sort_order",
                table: "master_menus",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0,
                oldComment: "ลำดับการแสดงภายใต้เมนูแม่เดียวกัน (น้อยก่อน)");

            migrationBuilder.AlterColumn<string>(
                name: "route",
                table: "master_menus",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "path ของหน้าจอที่เมนูนี้เปิด ว่างได้เมื่อเป็นเมนูกลุ่มที่ไม่มีหน้าของตัวเอง");

            migrationBuilder.AlterColumn<Guid>(
                name: "parent_id",
                table: "master_menus",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "เมนูแม่ (FK master_menus.id) NULL = เมนูระดับบนสุด");

            migrationBuilder.AlterColumn<string>(
                name: "name_th",
                table: "master_menus",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldComment: "ชื่อเมนูภาษาไทย");

            migrationBuilder.AlterColumn<string>(
                name: "name_en",
                table: "master_menus",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldComment: "ชื่อเมนูภาษาอังกฤษ");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "master_menus",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true,
                oldComment: "ซ่อน/แสดงเมนูโดยไม่ต้องลบ");

            migrationBuilder.AlterColumn<string>(
                name: "icon",
                table: "master_menus",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "ชื่อไอคอนที่แสดงข้างเมนู");

            migrationBuilder.AlterColumn<Guid>(
                name: "deleted_by",
                table: "master_menus",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่ลบแถวนี้ (soft delete)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "deleted_at",
                table: "master_menus",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldComment: "เวลาที่ลบแบบ soft delete ว่าง = ยังใช้งานอยู่");

            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "master_menus",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่สร้างแถวนี้ (ว่างได้สำหรับข้อมูลที่ระบบสร้าง)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "master_menus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่สร้างแถว (เก็บเป็น UTC)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "master_menus",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldComment: "รหัสเมนูสำหรับอ้างอิงข้ามระบบ ไม่ซ้ำ ไม่เป็นค่าว่าง");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "master_menus",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "รหัสของแถว (UUID)");

            migrationBuilder.AlterColumn<string>(
                name: "user_agent",
                table: "audit_logs",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldComment: "User-Agent ของผู้กระทำ");

            migrationBuilder.AlterColumn<Guid>(
                name: "platform_actor_id",
                table: "audit_logs",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "คอลัมน์จากแบบเดิมที่มีฐานกลาง ไม่มี FK (รอทบทวน)");

            migrationBuilder.AlterColumn<string>(
                name: "old_values",
                table: "audit_logs",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true,
                oldComment: "ค่าก่อนเปลี่ยน");

            migrationBuilder.AlterColumn<string>(
                name: "new_values",
                table: "audit_logs",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true,
                oldComment: "ค่าหลังเปลี่ยน");

            migrationBuilder.AlterColumn<string>(
                name: "ip_address",
                table: "audit_logs",
                type: "character varying(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(45)",
                oldMaxLength: 45,
                oldNullable: true,
                oldComment: "IP ของผู้กระทำ");

            migrationBuilder.AlterColumn<string>(
                name: "entity_type",
                table: "audit_logs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldComment: "ประเภทข้อมูลที่ถูกกระทำ เช่น role, user, menu");

            migrationBuilder.AlterColumn<Guid>(
                name: "entity_id",
                table: "audit_logs",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "รหัสของข้อมูลที่ถูกกระทำ");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "audit_logs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP",
                oldComment: "เวลาที่เกิดเหตุการณ์ (UTC)");

            migrationBuilder.AlterColumn<Guid>(
                name: "actor_user_id",
                table: "audit_logs",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "ผู้ใช้ที่ทำรายการ (FK users.id)");

            migrationBuilder.AlterColumn<string>(
                name: "actor_type",
                table: "audit_logs",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldComment: "ประเภทผู้กระทำ: user = ผู้ใช้ในระบบ, system = ระบบ, platform = ค่าจากแบบเดิม (รอทบทวน)");

            migrationBuilder.AlterColumn<string>(
                name: "action",
                table: "audit_logs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldComment: "ชื่อการกระทำ");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "audit_logs",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()",
                oldComment: "รหัสบันทึก audit (UUID)");
        }
    }
}
